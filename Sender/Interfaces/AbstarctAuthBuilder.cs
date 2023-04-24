using System;
using System.Net.Http.Headers;


namespace RequestSender
{
    public enum  AuthType
    {
        Basic,
        Bearer
    }

    public abstract class AbstarctAuthBuilder
    {
        protected AuthType _authType;

        public abstract AuthenticationHeaderValue Build();
        protected virtual AbstarctAuthBuilder SetAuthType() 
        {
            _authType = AuthType.Basic;
            return this;
        }

        public virtual AbstarctAuthBuilder SetBearerToken(string token) 
        {
            return this;
        }

        public virtual AbstarctAuthBuilder SetUserName(string username) 
        {
            return this;
        }

        public virtual AbstarctAuthBuilder SetPassword(string password) 
        {
            return this;
        }

    }
}
