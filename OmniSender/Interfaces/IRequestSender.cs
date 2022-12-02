using System;
using System.Threading.Tasks;

namespace OmniRequestSender
{
    public interface IRequestSender<Tout> 
    {
        Task<Tout> SendAsync(object requestData);
    }
}
