using System;
using System.Net.Http.Headers;

namespace OmniRequestSender
{
    public class BearerTokenAuthBuilder : AbstarctAuthBuilder
    {
        private string _bearerToken;

        public override AuthenticationHeaderValue Build()
        {

            SetAuthType();

            if (_bearerToken == "" || _bearerToken == null) 
            {
                throw new ArgumentNullException("bearerToken");
            }

            return new AuthenticationHeaderValue(_authType.ToString(), _bearerToken);
        }

        protected override AbstarctAuthBuilder SetAuthType()
        {
            _authType = AuthType.Bearer;
            return this;
        }

        public override AbstarctAuthBuilder SetBearerToken(string token)
        {
            _bearerToken = token;
            return this;
        }

    }
}
