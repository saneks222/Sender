using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OmniRequestSender.Helpers
{
    public static class ResponseHelper
    {
        public static HttpResponseMessage CancellMessage()
        {
            return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
        }
    }
}