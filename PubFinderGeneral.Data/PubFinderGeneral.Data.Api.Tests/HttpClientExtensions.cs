using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PubFinderGeneral.Data.Api.Tests
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> and_get(this HttpClient httpClient, string uri)
        {
            var message = GetMessage(HttpMethod.Get, uri);
            return await httpClient.SendMessage(message);
        }

        private static HttpRequestMessage GetMessage(HttpMethod method, string uri)
        {
            return new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"http://localhost{uri}")
            };
        }

        private static async Task<HttpResponseMessage> SendMessage(this HttpClient httpClient,
            HttpRequestMessage message)
        {
            return await httpClient.SendAsync(message);
        }
    }
}