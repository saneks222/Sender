using System;
using System.Threading;
using System.Threading.Tasks;

namespace RequestSender
{
    public interface IRequestSender<Tout> 
    {
        Task<Tout> SendAsync(object requestData,CancellationToken token);
    }
}
