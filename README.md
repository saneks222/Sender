# Содержание
+ Перечисления
  + AuthType  
+ Интерфейсы и абстрации
  + AbstarctAuthBuilder
  + IConverter
  + IHttpRequestData
  + IRequstSender
+ Конкретные реализации
  + BasicAuthBuilder
  + BearerTokenAuthBuilder
  + HttpRequestData
  + HttpSender
  + MockSender
+ Примеры вызова  
  + Mock запрос без конвертора
  + Mock запрос с конвертором
  + Http Запрос с базовой аутентификацией
  + Http запрос с Bearer аутентификацией
___
# Перечисления
## AuthType
### Поля
+ **Basic**
+ **Bearer**
___
# Интерфейсы и абстрации
## AbstarctAuthBuilder 
**Пространство имен**: OmniRequestSender

Представляет абстракную сущность для порождения конкретных реализаций строителей формирующих объеты для аутентификации 

```C#
public abstract class AbstarctAuthBuilder
```
### Поля и свойства 
+ **AuthType _authType** определяет тип аутентификации на основе перечисления **AuthType**
### Методы 
+ **AuthenticationHeaderValue Build()** конструирует и возвращает объект для аутентификации 
+ **AbstarctAuthBuilder SetAuthType()** устанавливает тип аутентификации
+ **AbstarctAuthBuilder SetBearerToken(string token)** устанавлевает токен для аутентификации 
+ **AbstarctAuthBuilder SetUserName(string username)** Устанавливает логин пользователя 
+ **AbstarctAuthBuilder SetPassword(string password)** Устанавливает пароль пользователя 

## IConverter
**Пространство имен**: OmniRequestSender

Представляет интерфейс для ковертации любого переданного типа в заданный 
```C#
public interface IConverter<Tout> where Tout : class
```
### Методы
+ **Tout Convert(object obj)** принемает обьект любого типа и конвертирует в заданный тип

## IHttpRequestData
**Пространство имен**: OmniRequestSender

Представляет интерфейс описывающий объект для отправки http запроса 
```C#
public interface IHttpRequestData
```
### Свойства
+ **string Url** содержет адрес запрашиваемого ресурса
+ **string Data** содержит страку с телом запросв
+ **Dictionary<string, string> Headers** содержит заголовки запроса
+ **AuthenticationHeaderValue Credentionals** содержит объет аутентификации для запроса

## IRequstSender
**Пространство имен**: OmniRequestSender

Представляет интерфейс отправителя запроса 
```C#
public interface IRequstSender<Tout> 
```
### Методы 
+ **Task<Tout> SendAsync(object requestData,CancellationToken token = default)** принимает объект, а так же токен отмены(опционально), запрса и отправляет запрос возвращая задынный тип ответа
___
# Конкретные реализации
## BasicAuthBuilder
**Пространство имен**: OmniRequestSender

Представляет класс унаследованный от **AbstarctAuthBuilder** реализует логику конструирования объекта с базовым типом аутентификации

```C#
public class BasicAuthBuilder : AbstarctAuthBuilder
```
### Поля и свойства 
+ **string _username** содержит логин пользователя 
+ **string _password** содержит пароль пользователя

### Методы 
+ **string GetBase64AuthString(string username, string password)** формирует base64String нужного формата из логина и пароля

## BearerTokenAuthBuilder
**Пространство имен**: OmniRequestSender

Представляет класс унаследованный от **AbstarctAuthBuilder** реализует логику конструирования объекта аутентификации на основе Bearer токена

```C#
public class BearerTokenAuthBuilder : AbstarctAuthBuilder
```
### Поля и свойства 
+ **string _bearerToken** содержит токен аутентификации

## HttpRequestData
**Пространство имен**: OmniRequestSender

Представляет конкретный класс с набором параметров для отправки Http запроса реализует интерфейс **_IHttpRequestData_**

```C#
public class HttpRequestData : IHttpRequestData
```
### Конструкторы
+ **HttpRequestData(string url, object data=null Dictionary<string, string> headers=null AuthenticationHeaderValue credentionals = null)**
+  **HttpRequestData(string url, Dictionary<string, string> headers, AuthenticationHeaderValue credentionals)**
+ **HttpRequestData(string url, object data, AuthenticationHeaderValue credentionals)**
+ **HttpRequestData(string url, object data,Dictionary<string, string> headers)**
+ **HttpRequestData(string url, AuthenticationHeaderValue credentionals)**
+ **HttpRequestData(string url, Dictionary<string, string> headers)**
+ **HttpRequestData(string url, object data)**

## HttpSender
**Пространство имен**: OmniRequestSender

Представляет класс реализующий логику отправки Http запроса имплементирует интерфейс **_IRequestSender<HttpResponseMessage>_**
```C#
public class HttpSender : IRequestSender<HttpResponseMessage>
```
### Конструкторы
+ **HttpSender(HttpMethod method)** 

### Поля и свойства
+ **HttpClient _client** экземпляр http клиента отправляющего запрос
+ **HttpMethod _method** экземпляр **HttpMethod** содержит информацию о типе запроса
+ **MediaTypeHeaderValue _mediaTypeHeaderValue экземпляр объекта содержащий contentType запроса
### Методы 
+ **async Task<HttpResponseMessage> SendAsync(object requestData,CancellationToken token = default)** реализует логику отправки http запроса,опционально принимает токен отмены операции.
+ **void SetHttpMethod(HttpMethod method)** устанавливает HttpMethod с которым будет отправляться запрос.
+ **void SetContentType(MediaTypeHeaderValue contentType)** устанавливает MediaTypeHeaderValue (contentType) с которым будет отправляться запрос. 

## MockSender
**Пространство имен**: OmniRequestSender

Представляет класс реализующий возможность имитации отправки запроса имплементирует интерфейс **_IRequstSender<Tout>_**
```C#
public class MockSender<Tout, Tin> : IRequestSender<Tout> where Tin : class
```

### Поля и свойства
+ **IConverter<Tin> _converter** содержет экземпляр конвертора для параметров запроса 
+ **Func<Tin, Tout> _executer** содержит функцию реализующую логику эмитации запроса

### Конструкторы
+ **MockSender(Func<Tin, Tout> executer)**
+ ***MockSender(IConverter<Tin> converter, Func<Tin, Tout> executer)**

В создания объекта с конструктором не принимающим конвертор будет произведена попытка приведения объекта с аргументами к типу **Tin**

### Методы 
+ **async Task<Tout> SendAsync(object requestData,CancellationToken token = default)** реазлизует логику отправки запроса вызывая ***_executer* с которым был сформирован объект,в случае отмены операции через CancellationToken выбрасывает исключение taskcanceledexception.
+ **void SetConverter(IConverter<Tin> converter)** устанавливает конвертор с которым будет вызываться запрос.
___
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
