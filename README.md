# Примеры вызова
## Mock запрос без конвертора
```C#
 public class MyParametrs
    {
        public int x { get; set; }
        public int y { get; set; }

        public MyParametrs(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

var sender1 = new MockSender<List<int>, MyParametrs>(prm =>
            {
                var l = new List<int>();
                l.Add(prm.x + 7);
                l.Add(prm.y + 98);
                return l;
            });
            var result1 = sender1.SendAsync(new MyParametrs(1, 2));
            result1.Wait();
            Console.WriteLine(result1.Result[0] + " " + result1.Result[1]);
```
## Mock запрос с конвертором 
```C#
public class MyParametrs
    {
        public int x { get; set; }
        public int y { get; set; }

        public MyParametrs(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class MyConvert : IConverter<MyParametrs>
    {
        public MyParametrs Convert(object obj)
        {
            var jsonString = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<MyParametrs>(jsonString);
        }
    }

    var sender = new MockSender<List<int>, MyParametrs>(new MyConvert(), prm =>
            {
                var l = new List<int>();
                l.Add(prm.x + 7);
                l.Add(prm.y + 98);
                return l;
            });

            var param = new { x = 1, y = 2 };

            var result = sender.SendAsync(param);
            result.Wait();

            Console.WriteLine(result.Result[0] + " " + result.Result[1]);
```
## Http Запрос с базовой аутентификацией 
```C#
var senderBasic = new HttpSender(HttpMethod.Post);
            var auth = new BasicAuthBuilder().SetPassword("qwerty").SetUserName("Aleksander").Build();
            var headers = new Dictionary<string, string>();
            headers.Add("custumHeader", "value123");
            var basicParam = new HttpRequestData(url, new { TestData = "helloWorld" }, headers, auth);
            var resultBasic = senderBasic.SendAsync(basicParam);
            resultBasic.Wait();
```
## Http запрос с Bearer аутентификацией 
```C#
var senderBearer = new HttpSender(HttpMethod.Post);
            var authBearer = new BearerTokenAuthBuilder().SetBearerToken("qwertyusdfs").Build();
            var headers2 = new Dictionary<string, string>();
            headers2.Add("custumHeader2", "value321");
            var bearerParam = new HttpRequestData(url, new { TestData = "helloWorld2" }, headers2, authBearer);
            var resultBearer = senderBearer.SendAsync(bearerParam);
            resultBearer.Wait();
```
___
