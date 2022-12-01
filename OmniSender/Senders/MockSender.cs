using System;
using System.Threading.Tasks;

namespace OmniRequestSender
{
    public class MockSender<Tout, Tin> : IRequstSender<Tout> where Tin : class
    {

        IConverter<Tin> _converter;

        Func<Tin, Tout> _executer;

        public MockSender(Func<Tin, Tout> executer)
        {
            _executer = executer;
        }
        public MockSender(IConverter<Tin> converter, Func<Tin, Tout> executer)
        {
            _converter = converter;
            _executer = executer;
        }

        public async Task<Tout> SendAsync(object requestData)
        {

            if (_converter == null)
            {
                var data = (Tin)requestData;
                return _executer(data);
            }
            else
            {
                var data = _converter.Convert(requestData);
                return _executer(data);
            }
        }
    }
}
