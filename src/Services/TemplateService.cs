using Konso.Clients.Messaging.Extensions;
using Konso.Clients.Messaging.Interfaces;
using Konso.Clients.Messaging.Model.Dtos;
using Konso.Clients.Messagings.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Konso.Clients.Messaging.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly MessagingConfig _messagingConfig;

        public TemplateService(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _messagingConfig = new MessagingConfig();

            _messagingConfig.Endpoint = configuration.GetValue<string>("Konso:Messaging:Endpoint");
            _messagingConfig.BucketId = configuration.GetValue<string>("Konso:Messaging:BucketId");
            _messagingConfig.ApiKey = configuration.GetValue<string>("Konso:Messaging:ApiKey");
            _messagingConfig.Environment = configuration.GetValue<string>("Konso:Messaging:Env");
            _messagingConfig.Endpoint = _messagingConfig.Endpoint.RemoveTailSlash();
            _clientFactory = clientFactory;
        }

        public TemplateService(MessagingConfig messagingConfig, IHttpClientFactory clientFactory)
        {
            _messagingConfig = messagingConfig;
            _clientFactory = clientFactory;
        }


        public async Task<PagedResponse<KonsoTemplateDto>> GetAsync(byte? targetId, int from = 0, int to = 10)
        {
            try
            {
                var client = _clientFactory.CreateClient();

                if (string.IsNullOrEmpty(_messagingConfig.Endpoint)) throw new Exception("Endpoint is not defined");
                if (string.IsNullOrEmpty(_messagingConfig.BucketId)) throw new Exception("Bucket is not defined");
                if (string.IsNullOrEmpty(_messagingConfig.ApiKey)) throw new Exception("API key is not defined");
                if (!client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", _messagingConfig.ApiKey)) throw new Exception("Missing API key");

                var builder = new UriBuilder($"{_messagingConfig.Endpoint}/cms/templates/{_messagingConfig.BucketId}")
                {
                    Port = -1
                };
                var query = HttpUtility.ParseQueryString(builder.Query);


                if (targetId.HasValue)
                {
                    query["targetId"] = targetId.Value.ToString();
                }

                query["from"] = from.ToString();
                query["to"] = to.ToString();

                builder.Query = query.ToString();
                string url = builder.ToString();

                string responseBody = await client.GetStringAsync(url);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var responseObj = JsonSerializer.Deserialize<PagedResponse<KonsoTemplateDto>>(responseBody, options);
                return responseObj;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
