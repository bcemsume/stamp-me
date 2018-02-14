using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StampMe.Business.Abstract;
using StampMe.Common.CustomDTO;
using StampMe.Common.MessageLoggingHandler;

namespace StampMe.API
{

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public CustomExceptionFilterAttribute(
            IHostingEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public override void OnException(ExceptionContext context)
        {
            if (!_hostingEnvironment.IsDevelopment())
            {
                // do nothing
                return;
            }
            var result = new ViewResult { ViewName = "CustomError" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
            result.ViewData.Add("Exception", context.Exception);
            // TODO: Pass additional detailed data via ViewData
            context.Result = result;
        }
    }


    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";

        public HttpStatusCodeException(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, Exception inner) : this(statusCode, inner.ToString()) { }

        public HttpStatusCodeException(int statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }
    }

    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                if (context.Response.HasStarted)
                {
                    // _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = ex.ContentType;

                await context.Response.WriteAsync(ex.Message);

                return;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
        }
    }

    public class MessageLogging : MessageHandler
    {
        ILogDataService _logDataService;

        public MessageLogging(ILogDataService logDataService)
        {
            _logDataService = logDataService;
        }

        protected override async Task IncommingMessageAsync(DateTime date, string correlationId, string requestInfo, string ipAdress, byte[] message, string UserSecurityKey)
        {
            var logData = new LogDataDTO
            {
                Id = ObjectId.GenerateNewId(),
                LogDate = date,
                CorrelationId = correlationId,
                RequestInfo = requestInfo,
                IpAdress = ipAdress,
                Message = Encoding.UTF8.GetString(message),
                Type = 0,
                StatusCode = "Request",
            };

            await _logDataService.Add(logData);

        }

        protected override async Task OutgoingMessageAsync(DateTime date, string correlationId, string requestInfo, string ipAdress, byte[] message, long ElapsedTime, string UserSecurityKey, HttpStatusCode statusCode)
        {
            var logData = new LogDataDTO
            {
                Id = ObjectId.GenerateNewId(),
                LogDate = date,
                CorrelationId = correlationId,
                RequestInfo = requestInfo,
                IpAdress = ipAdress,
                Message = Encoding.UTF8.GetString(message),
                ElapsedTime = ElapsedTime,
                Type = 1,
                StatusCode = statusCode.ToString(),
            };

            await _logDataService.Add(logData);
        }
    }
}
