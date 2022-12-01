using System;
using System.Text;
using System.Net.Http.Headers;


namespace OmniRequestSender
{
    public class BasicAuthBuilder : AbstarctAuthBuilder
    {
        private string _username;
        private string _password;
        public override AuthenticationHeaderValue Build()
        {
            SetAuthType();

            string authParam = GetBase64AuthString(_username,_password);
            return new AuthenticationHeaderValue(_authType.ToString(),authParam);
        }

        protected override AbstarctAuthBuilder SetAuthType()
        {
            _authType=AuthType.Basic;
            return this;
        }

        public override AbstarctAuthBuilder SetUserName(string username)
        {
            _username = username;
            return this;
        }

        public override AbstarctAuthBuilder SetPassword(string password)
        {
            _password = password;
            return this;
        }

        private string GetBase64AuthString(string username, string password)
        {
            if(username==""||username==null||password==""||password==null)
                throw new ArgumentNullException("username or password");

            string data = username+":"+password;
            var dataByte = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(dataByte);
        }
    }
}
