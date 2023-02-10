using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OmniRequestSender
{
    public class HttpSender : IRequestSender<HttpResponseMessage>
    {
        private HttpClient _client = new HttpClient();
        private HttpMethod _method;

        public HttpSender(HttpMethod method) 
        {
            _method = method;
        }

        public async Task<HttpResponseMessage> SendAsync(object requestData)
        {
            var data = (IHttpRequestData)requestData;

            if (_method == HttpMethod.Get && data.Credentionals==null) 
            {
                return await _client.GetAsync(data.Url);
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

                var response = await _client.SendAsync(requestMsg);
                   return response;
                
            }
        }

    }
}
