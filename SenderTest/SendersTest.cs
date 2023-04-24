using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SenderTest
{
    public class SendersTest
    {
        private int _x = 4;
        private int _y = 4;
        private string _url = @"https://www.yandex.ru";
        private MockSender<int, Params> _mockSender;
        private HttpSender _httpSender;
        private int Actual { get => this._x * this._y; }

        [SetUp]
        public void Setup()
        {
            var executer = (Params param) => 
            {
                return param.y * param.x;
            };
            _mockSender = new(executer);

            _httpSender = new HttpSender(HttpMethod.Get);
        }

        [Test]
        public void TrySendMockSenderWithOutConverter()
        {
            var task = _mockSender.SendAsync(new Params(this._x,this._y));
            task.Wait();
            var result = task.Result;

            Assert.AreEqual(result,Actual);
            Assert.AreNotEqual(result, Actual - 1);
        }

        [Test]
        public void TrySendMockSenderWithConverter()
        {
            _mockSender.SetConverter(new ObjToParamsConverter());
            var obj = new {key = this._x,value = this._y };
            var task = _mockSender.SendAsync(obj);
            task.Wait();
            _mockSender.SetConverter(null);
            var result = task.Result;

            Assert.AreEqual(result, Actual);
            Assert.AreNotEqual(result, Actual - 1);
        }
        
        [Test]
        public void SendHttpGet() 
        {
            var data = new HttpRequestData(_url);
            var task = _httpSender.SendAsync(data);
            task.Wait();
            Assert.AreEqual(task.Result.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void SendHttpPost()
        {
            var data = new HttpRequestData(_url);
            _httpSender.SetHttpMethod(HttpMethod.Post);
            var task = _httpSender.SendAsync(data);
            task.Wait();
            Assert.AreEqual(task.Result.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void SendHttpPut()
        {
            var data = new HttpRequestData(_url);
            _httpSender.SetHttpMethod(HttpMethod.Put);
            var task = _httpSender.SendAsync(data);
            task.Wait();
            Assert.AreEqual(task.Result.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void SendHttpPatch()
        {
            var data = new HttpRequestData(_url);
            _httpSender.SetHttpMethod(HttpMethod.Patch);
            var task = _httpSender.SendAsync(data);
            task.Wait();
            Assert.AreEqual(task.Result.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void SendHttpDelete()
        {
            var data = new HttpRequestData(_url);
            _httpSender.SetHttpMethod(HttpMethod.Delete);
            var task = _httpSender.SendAsync(data);
            task.Wait();
            Assert.AreEqual(task.Result.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void SendHttpOptions()
        {
            var data = new HttpRequestData(_url);
            _httpSender.SetHttpMethod(HttpMethod.Options);
            var task = _httpSender.SendAsync(data);
            task.Wait();
            Assert.AreEqual(task.Result.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void SendHttpHead()
        {
            var data = new HttpRequestData(_url);
            _httpSender.SetHttpMethod(HttpMethod.Head);
            var task = _httpSender.SendAsync(data);
            task.Wait();
            Assert.AreEqual(task.Result.StatusCode, HttpStatusCode.OK);
        }

        internal class Params 
        {
            public int x { get; set; }
            public int y { get; set; }

            public Params(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        internal class ObjToParamsConverter : IConverter<Params>
        {
            public Params Convert(object obj)
            {
                var jsonString = JsonConvert.SerializeObject(obj);
                KeyValuePair<string,string> keyValuePair = JsonConvert.DeserializeObject<KeyValuePair<string,string>>(jsonString);
                int x = int.Parse(keyValuePair.Key);
                int y = int.Parse(keyValuePair.Value);
                return new Params(x,y);
            }
        }
    }
}