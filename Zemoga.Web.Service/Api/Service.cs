using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Zemoga.Web.Service
{
    public abstract class Service<T> where T : class
    {
        protected async Task<R> MakeRequest<R>(
            HttpRequestMethod method,
            string url,
            object body = null)
            where R : class
        {
            var client = new RestClient(ConfigurationManager.AppSettings["ZemogaServiceUrl"]);
            var request = new RestRequest(url, this.MethodFromHttpRequestMethod(method));
            request.AddHeader("content-type", "application/json");
            if (body != null)
            {
                request.AddJsonBody(body);
            }

            ManualResetEventSlim awaiter = new ManualResetEventSlim(false);
            R data = null;

            await Task.Factory.StartNew(() =>
            {
                client.ExecuteAsync(request, response =>
                {
                    if (method == HttpRequestMethod.GET && response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        data = null;
                    }
                    else if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        data = null;
                    }
                    else if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                    {
                        data = JsonConvert.DeserializeObject<R>(response.Content);
                    }

                    awaiter.Set();
                });
            });

            awaiter.Wait();
            return data;
        }

        private Method MethodFromHttpRequestMethod(HttpRequestMethod method)
        {
            switch (method)
            {
                case HttpRequestMethod.DELETE:
                    return Method.DELETE;

                case HttpRequestMethod.GET:
                    return Method.GET;

                case HttpRequestMethod.POST:
                    return Method.POST;

                case HttpRequestMethod.PUT:
                    return Method.PUT;

                default:
                    throw new NotSupportedException("Method not supported.");
            }
        }

        public enum HttpRequestMethod
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
