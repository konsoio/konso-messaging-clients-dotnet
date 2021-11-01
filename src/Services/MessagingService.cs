using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Konso.Clients.Messagings.Interfaces;
using Konso.Clients.Messagings.Model;
using Konso.Clients.Messagings.Model.Dtos;
using Konso.Clients.Messagings.Model.Requests;
using Microsoft.Extensions.Configuration;

namespace Konso.Clients.Messagings.Services.Requests
{
    public class MessagingService : IMessagingService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly MessagingConfig _messagingConfig;
 
        public MessagingService(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _messagingConfig = new MessagingConfig();

            _messagingConfig.Endpoint = configuration.GetValue<string>("Konso:Messaging:Endpoint");
            _messagingConfig.BucketId = configuration.GetValue<string>("Konso:Messaging:BucketId");
            _messagingConfig.ApiKey = configuration.GetValue<string>("Konso:Messaging:ApiKey");
            _messagingConfig.Environment = configuration.GetValue<string>("Konso:Messaging:Env");
            _clientFactory = clientFactory;
        }

        public MessagingService(MessagingConfig messagingConfig, IHttpClientFactory clientFactory)
        {
            _messagingConfig=messagingConfig;
            _clientFactory = clientFactory;
        }
        public async Task<bool> SendAsync(CreateMessageRequest request)
        {
            try
            {
                var msg = new MessageRequestDto() 
                { 
                    BucketId = _messagingConfig.BucketId, 
                    Subject = request.Subject, 
                    CorrelationId = request.CorrelationId, 
                    Env=_messagingConfig.Environment, 
                    Recipients = request.Recipients,
                    Delay = request.Delay,
                    MessageType = request.MessageType,
                    Tags = request.Tags
                };

                // to base64
                if (!string.IsNullOrEmpty(request.Html))
                {
                    msg.HtmlBase64Body = Base64Encode(request.Html);
                }

                if (!string.IsNullOrEmpty(request.PlainText))
                {
                    msg.PlainBase64Body = Base64Encode(request.PlainText);
                }

                using (var _client = _clientFactory.CreateClient())
                {
                    if (!_client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", _messagingConfig.ApiKey)) throw new Exception("Missing API key");
                    // serialize request as json
                    var jsonStr = JsonSerializer.Serialize(msg);
                    var httpItem = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync($"{_messagingConfig.Endpoint}/messaging/{_messagingConfig.BucketId}", httpItem);
                    var contents = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<GenericResponse<bool>>(contents);

                    if (!result.Succeeded)
                        throw new Exception(string.Format("Error sending value tracking {0}", result.ValidationErrors[0].Message));
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public async Task<PagedResponse<MessageHistoryDto>> GetAsync(MessageHistoryGetRequest request)
        {
            try
            {
                var client = new HttpClient();

                if (string.IsNullOrEmpty(_messagingConfig.Endpoint)) throw new Exception("Endpoint is not defined");
                if (string.IsNullOrEmpty(_messagingConfig.BucketId)) throw new Exception("Bucket is not defined");
                if (string.IsNullOrEmpty(_messagingConfig.ApiKey)) throw new Exception("API key is not defined");
                if (!client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", _messagingConfig.ApiKey)) throw new Exception("Missing API key");

                int sortNum = (int)request.Sort;
                var builder = new UriBuilder($"{_messagingConfig.Endpoint}/messaging_history/{_messagingConfig.BucketId}")
                {
                    Port = -1
                };
                var query = HttpUtility.ParseQueryString(builder.Query);

                if (!string.IsNullOrEmpty(request.Email))
                    query["email"] = request.Email;

                if (!string.IsNullOrEmpty(request.Subject))
                    query["subject"] = request.Subject;

                if (request.DateFrom.HasValue)
                    query["fromDate"] = request.DateFrom.ToString();

                if (request.DateTo.HasValue)
                    query["toDate"] = request.DateTo.ToString();
                if (sortNum > 0)
                    query["sort"] = sortNum.ToString();
                query["from"] = request.From.ToString();
                query["to"] = request.To.ToString();
                
                if (request.Tags != null && request.Tags.Count > 0)
                {
                    query["tags"] = String.Join(",", request.Tags);
                }

                builder.Query = query.ToString();
                string url = builder.ToString();

                string responseBody = await client.GetStringAsync(url);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var responseObj = JsonSerializer.Deserialize<PagedResponse<MessageHistoryDto>>(responseBody, options);
                return responseObj;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
