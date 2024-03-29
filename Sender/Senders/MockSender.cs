﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace RequestSender
{
    public class MockSender<Tout, Tin> : IRequestSender<Tout> where Tin : class
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

        public void SetConverter(IConverter<Tin> converter) 
        {
            _converter= converter;
        }

        public async Task<Tout> SendAsync(object requestData,CancellationToken token=default)
        {
            if (token.IsCancellationRequested)
                throw new OperationCanceledException();

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
