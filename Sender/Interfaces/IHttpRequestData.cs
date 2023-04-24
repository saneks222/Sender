using System.Net.Http.Headers;
using System.Collections.Generic;

namespace RequestSender
{
    public interface IHttpRequestData
    {
        string Url { get;  }
        string Data { get;  }
        Dictionary<string, string> Headers { get; }
        AuthenticationHeaderValue Credentionals { get;  }
    }
}