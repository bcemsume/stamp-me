using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StampMe.Common.MessageLoggingHandler
{
    public abstract class MessageHandler : DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var corrId = string.Format("{0}", Thread.CurrentThread.ManagedThreadId);
            var requestInfo = string.Format("{0} {1}", request.Method, request.RequestUri);

            var requestMessage = await request.Content.ReadAsByteArrayAsync();



            await IncommingMessageAsync(DateTime.Now, corrId, requestInfo, request.GetClientIpAddress(), requestMessage, "");

            var watcher = Stopwatch.StartNew();

            var response = await base.SendAsync(request, cancellationToken);

            watcher.Stop();

            byte[] responseMessage;

            if (response.IsSuccessStatusCode)
                responseMessage = response.Content != null ? await response.Content.ReadAsByteArrayAsync() : Encoding.UTF8.GetBytes("");
            else
                responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);

            await OutgoingMessageAsync(DateTime.Now, corrId, requestInfo, request.GetClientIpAddress(), responseMessage, watcher.ElapsedMilliseconds, "", response.StatusCode);
            return response;

        }

        protected abstract Task IncommingMessageAsync(DateTime date, string correlationId, string requestInfo, string ipAdress, byte[] message, string UserSecurityKey);
        protected abstract Task OutgoingMessageAsync(DateTime date, string correlationId, string requestInfo, string ipAdress, byte[] message, long ElapsedTime, string UserSecurityKey, HttpStatusCode statusCode);
    }


    public static class HttpRequestMessageExtensions
    {
        private const string HttpContext = "MS_OwinContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.RemoteIpAddress;
                }
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            return null;
        }
    }
}
