using System.Net.Http;

namespace Konso.Clients.Messagings.Tests
{
    public sealed class DefaultHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name) => new HttpClient();
    }
}
