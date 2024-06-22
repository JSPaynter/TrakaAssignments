using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Web;

namespace Core.BaseClasses
{
    public class BaseAPI
    {
        protected readonly HttpClient client;

        public BaseAPI(string baseUrl)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public BaseAPI(HttpClient client)
        {
            this.client = client;
        }

        protected void ResetClient()
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
        }

        protected void AddHeader(string key, string value)
        {
            client.DefaultRequestHeaders.Add(key, value);
        }

        protected void AddAcceptHeader(string value)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(value));
        }

        protected string BuildParameterString(Dictionary<string, object> parameters)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach (var key in parameters.Keys)
                query[key] = parameters[key].ToString();

            return query.ToString();
        }

        public HttpResponseMessage Get(string url,  string? parameters=null)
        {
            if (parameters != null) 
                url += "?" + parameters;
            return client.GetAsync(url).Result;
        }

        public HttpResponseMessage Get(string url, Dictionary<string, object> parameters)
        {
            string parameterString = "";
            if (parameters.Count > 0)
                parameterString = BuildParameterString(parameters);
            return Get(url, parameterString);
        }

        public HttpResponseMessage Post(string? parameters=null, StringContent? content=null)
        {
            string url = "";
            if (parameters != null)
                url += "?" + parameters;
            return client.PostAsync(url, content).Result;
        }

        public HttpResponseMessage Post(Dictionary<string, object> parameters, StringContent? content = null)
        {
            string parameterString = "";
            if (parameters.Count > 0)
                parameterString = BuildParameterString(parameters);
            return Post(parameterString, content);
        }

        public HttpResponseMessage Put(string uri, string? parameters = null, StringContent? content = null)
        {
            if (parameters != null)
                uri += "?" + parameters;
            return client.PutAsync(uri, content).Result;
        }

        public HttpResponseMessage Put(string uri, Dictionary<string, object> parameters, StringContent? content = null)
        {
            string parameterString = "";
            if (parameters.Count > 0)
                parameterString = BuildParameterString(parameters);
            return Put(uri, parameterString, content);
        }

        public HttpResponseMessage Delete(string uri, string? parameters = null)
        {
            if (!string.IsNullOrWhiteSpace(parameters))
                uri += "?" + parameters;
            return client.DeleteAsync(uri).Result;
        }

        public HttpResponseMessage Delete(string uri, Dictionary<string, object> parameters)
        {
            string parameterString = "";
            if (parameters.Count > 0)
                parameterString = BuildParameterString(parameters);
            return Delete(uri, parameterString);
        }
    }
}
