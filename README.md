

# Konso Messaging .Net Client

A .NET 5 Open Source Client Library for [KonsoApp](https://app.konso.io)

✅ Developed / Developing by [InDevLabs](https://indevlabs.de)


### Installation

⚠️ Prerequisites: *Konso account and created bucket* 

In order to use this library, you need reference it in your project.

Add config to `appsettings.json`:
```
"Konso": {
    "Messaging": {
        "Endpoint": "https://apis.konso.io",
        "BucketId": "<your bucket id>",
        "ApiKey": "<bucket's access key>",
        "Env": "DEV"
    }
}
```


It uses `HttpClientFactory`:

```
public void ConfigureServices(IServiceCollection services)
{
    // use httpclient factory
    services.AddHttpClient();
}
```

Resolve the service in class constractor:
```
private readonly IMessagingService _messagingService;
public SomeClass(IMessagingService messagingService)
{
    _messagingService = messagingService;
}
```

To send message:
```
await _messagingService.SendAsync(new CreateMessageRequest() 
{ 
    MessageType = MessageTypes.Email, 
    Subject = "Test subject", 
    Recipients = new List<string>() { "test@indevlabs.de" }, 
    Html = "<h1>Hello</h1>", 
    Tags = new List<string>() { "test" } 
});
```
