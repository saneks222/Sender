using System;

namespace RequestSender
{
    public interface IConverter<Tout> where Tout : class
    {
        Tout Convert(object obj);
    }
}
