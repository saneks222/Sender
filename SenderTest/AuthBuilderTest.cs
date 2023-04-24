using System.Net.Http.Headers;
using System.Text;

namespace SenderTest
{
    public class AuthBuilderTest
    {
        private string cred = "test"; 
        public AuthenticationHeaderValue BasicAuth { get; set; }
        public AuthenticationHeaderValue BearerAuth { get; set; }
        [SetUp]
        public void Setup()
        {
            //basic
            string data = cred + ":" + cred;
            var dataByte = Encoding.UTF8.GetBytes(data);
            BasicAuth = new AuthenticationHeaderValue(AuthType.Basic.ToString(), Convert.ToBase64String(dataByte));

            //bearer
            BearerAuth = new AuthenticationHeaderValue(AuthType.Bearer.ToString(),cred);
        }

        [Test]
        public void PrepareBasicAuthAndCheckThatEqualTemplate()
        {
            var param = new BasicAuthBuilder().SetPassword(cred).SetUserName(cred).Build();
            Assert.AreEqual(param,BasicAuth);
        }

        [Test]
        public void PrepareBasicAuthAndCheckThatNotEqualTemplate()
        {
            var param = new BasicAuthBuilder().SetPassword(cred+cred).SetUserName(cred).Build();
            Assert.AreNotEqual(param, BasicAuth);
        }

        [Test]
        public void PrepareBearerAuthAndCheckThatEqualTemplate()
        {
            var param = new BearerTokenAuthBuilder().SetBearerToken(cred).Build();
            Assert.AreEqual(param, BearerAuth);
        }

        [Test]
        public void PrepareBearerAuthAndCheckThatNotEqualTemplate()
        {
            var param = new BearerTokenAuthBuilder().SetBearerToken(cred+cred).Build();
            Assert.AreNotEqual(param, BearerAuth);
        }
    }
}