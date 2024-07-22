using FluentAssertions;
using Konso.Clients.Messaging.Services;
using Konso.Clients.Messagings.Model;
using System.Threading.Tasks;
using Xunit;

namespace Konso.Clients.Messagings.Tests
{
    public class TemplateServiceTests
    {
        private const string apiUrl = "https://apis-weu-dev.konso.io";
        private const string bucketId = "32f275e0";
        private const string apiKey = "PhPwsPhb0Q/7om9jtcuBhH+OpPC3LSbPmsOK7tovnP4=";


        [Fact]
        public async Task Get_Tempaltes ()
        {
            var service = new TemplateService(new MessagingConfig() { ApiKey = apiKey, BucketId = bucketId, Endpoint = apiUrl, Environment = "dev" },
                new DefaultHttpClientFactory());

            var response = await service.GetAsync(1, 0 ,1);

            // 
            response.List.Count.Should().NotBe(0);
        }

    }
}
