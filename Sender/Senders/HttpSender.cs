using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using RequestSender.Helpers;


namespace RequestSender
{
    public class HttpSender : IRequestSender<HttpResponseMessage>
    {
        private HttpClient _client = new HttpClient();
        private HttpMethod _method;
        private MediaTypeHeaderValue _mediaTypeHeaderValue;

        public HttpSender(HttpMethod method) 
        {
            _method = method;
            _mediaTypeHeaderValue = null;
        }

        public HttpSender(HttpMethod method, MediaTypeHeaderValue contentType)
        {
            _method = method;
            _mediaTypeHeaderValue = contentType;
        }

        public void SetHttpMethod(HttpMethod method) 
        {
            _method=method;
        }

        public void SetContentType(MediaTypeHeaderValue contentType)
        {
            _mediaTypeHeaderValue = contentType;
        }

        public async Task<HttpResponseMessage> SendAsync(object requestData,CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
                return ResponseHelper.CancellMessage(); 

            var data = (IHttpRequestData)requestData;

            if (_method == HttpMethod.Get && data.Credentionals==null) 
            {
                return await _client.GetAsync(data.Url, token);
            }

            using (var requestMsg = new HttpRequestMessage(_method, data.Url))
            {
                if (data.Credentionals != null)
                    requestMsg.Headers.Authorization = data.Credentionals;

                if (data.Headers != null && data.Headers.Count > 0)
                {
                    foreach (var heder in data.Headers)
                    {
                        requestMsg.Headers.Add(heder.Key, heder.Value);
                    }
                }

                if (data.Data != null&& data.Data!="")
                    requestMsg.Content = new StringContent(data.Data);

                if(_mediaTypeHeaderValue!=null)
                    requestMsg.Content.Headers.ContentType = _mediaTypeHeaderValue;

                var response = await _client.SendAsync(requestMsg, token);
                   return response;
                
            }
        }
    }
}
