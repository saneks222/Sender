using System;
using System.Threading.Tasks;

namespace OmniRequestSender
{
    public interface IRequstSender<Tout> 
    {
        Task<Tout> SendAsync(object requestData);
    }
}
