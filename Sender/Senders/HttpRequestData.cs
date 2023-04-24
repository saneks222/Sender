using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;


namespace RequestSender
{
    public class HttpRequestData : IHttpRequestData
    {
        public string Url { get; private set; }
        public string Data { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }
        public AuthenticationHeaderValue Credentionals { get; private set; }

        
        public HttpRequestData(string url, object data=null,
            Dictionary<string, string> headers=null, AuthenticationHeaderValue credentionals = null)
        {
            Url = url;
            Data = JsonConvert.SerializeObject(data);
            Headers = headers;
            Credentionals = credentionals;

        }

        public HttpRequestData(string url, Dictionary<string, string> headers, AuthenticationHeaderValue credentionals)
        {
            Url = url;
            Headers = headers;
            Credentionals = credentionals;
        }
        public HttpRequestData(string url, object data, AuthenticationHeaderValue credentionals)
        {
            Url = url;
            Data = JsonConvert.SerializeObject(data);
            Credentionals=credentionals;
        }

        public HttpRequestData(string url, object data,Dictionary<string, string> headers)
        {
            Url = url;
            Data = JsonConvert.SerializeObject(data);
            Headers = headers;
        }

        public HttpRequestData(string url, AuthenticationHeaderValue credentionals)
        {
            Url = url;
            Credentionals = credentionals;
        }

        public HttpRequestData(string url, Dictionary<string, string> headers)
        {
            Url = url;
            Headers = headers;
        }

        public HttpRequestData(string url, object data)
        {
            Url = url;
            Data = JsonConvert.SerializeObject(data);
        }
    }
}
