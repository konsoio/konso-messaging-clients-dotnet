using FluentAssertions;
using Konso.Clients.Messagings.Model;
using Konso.Clients.Messagings.Model.Enums;
using Konso.Clients.Messagings.Model.Requests;
using Konso.Clients.Messagings.Services.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Konso.Clients.Messagings.Tests
{
    public class MessagingServiceTests
    {
        private const string apiUrl = "https://apis.konso.io";
        private const string bucketId = "<bucketId>";
        private const string apiKey = "<your api key>";


        [Fact]
        public async Task Create_SimpleRequest()
        {
            var service = new MessagingService(new MessagingConfig() { ApiKey = apiKey, BucketId = bucketId, Endpoint = apiUrl, Environment = "dev" },
                new DefaultHttpClientFactory());

            var o = new CreateMessageRequest() {  MessageType = MessageTypes.Email, Subject = "Test subject", Recipients = new List<string>() { "test@indevlabs.de" }, Html = "<h1>Hello</h1>", Tags = new List<string>() { "test" } };

            var response = await service.SendAsync(o);

            // 
            response.Should().BeTrue();
        }

        [Fact]
        public async Task Create_AndGetSimpleRequest()
        {
            var service = new MessagingService(new MessagingConfig() { ApiKey = apiKey, BucketId = bucketId, Endpoint = apiUrl, Environment = "dev" },
                   new DefaultHttpClientFactory());

            var o = new CreateMessageRequest() { MessageType = MessageTypes.Email, Subject = "Test subject", Recipients = new List<string>() { "test@indevlabs.de" }, Html = "<h1>Hello</h1>",  Tags = new List<string>() { "test" } };

            var response = await service.SendAsync(o);

            // 
            var getResponse = await service.GetAsync(new MessageHistoryGetRequest() { From = 0, To = 10 });
        }
    }
}
