using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Newtonsoft.Json;
using StampMe.Business.Abstract;
using StampMe.Business.Concrete;
using StampMe.Common.MessageLoggingHandler;
using StampMe.DataAccess.Abstract;
using StampMe.DataAccess.Concrete;
using Swashbuckle.AspNetCore.Swagger;

namespace StampMe.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "hupp API", Version = "v1" });
            });



            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDal, MongoUserDal>();

            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IRestaurantDal, MongoRestaurantDal>();

            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IContractDal, MongoContractDal>();

            services.AddScoped<IRewardDetailDal, MongoRewardDetailDal>();

            services.AddScoped<ILogDataDal, MongoLogDataDal>();
            services.AddScoped<ILogDataService, LogDataService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseMiddleware<ErrorLoggingMiddleware>();
            app.UseMvc();

            app.UseCors("MyPolicy");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Restaurant}/{action=Get}/{id?}");
            });
            app.UseSwagger();
            app.UseStaticFiles();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "hupp API v1");
            });


        }
    }

    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusCodeException e)
            {
                LogDataService logDataService = new LogDataService(new MongoLogDataDal());
                await logDataService.Add(new Common.CustomDTO.LogDataDTO()
                {
                    Message = e.Message+"\r\n"+e.StackTrace+"\r\n"+ (e.InnerException != null ? e.InnerException.Message : ""),
                    CorrelationId = context.Connection.Id,
                    IpAdress = context.Connection.LocalIpAddress + "#" + context.Connection.RemoteIpAddress,
                    RequestInfo = context.Request.Path,
                    Type = context.Request.Method == "GET" ? 0 : 1,
                    LogDate = DateTime.Now,
                    StatusCode = "400",
                    Id = ObjectId.GenerateNewId(),
                    ElapsedTime = 1
                });
                context.Response.Clear();
                context.Response.StatusCode = e.StatusCode;
                context.Response.ContentType = e.ContentType;

                await context.Response.WriteAsync(e.Message);
            }
        }
    }

    public static class ErrorLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }


}
