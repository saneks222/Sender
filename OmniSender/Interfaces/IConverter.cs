using System;

namespace OmniRequestSender
{
    public interface IConverter<Tout> where Tout : class
    {
        Tout Convert(object obj);
    }
}
