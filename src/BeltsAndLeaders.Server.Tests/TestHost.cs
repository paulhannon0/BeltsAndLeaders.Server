using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BeltsAndLeaders.Server.Tests
{
    public class TestHost
    {
        public string EndpointPath { get; set; }

        public IDictionary<string, object> RequestBody { get; set; }

        public HttpResponseMessage LastResponseMessage { get; set; }

        private readonly HttpClient client;

        public TestHost()
        {
            this.RequestBody = new Dictionary<string, object>();
            this.client = new WebApplicationFactory<Startup>().CreateClient();
        }

        public async Task<HttpResponseMessage> PostAsync()
        {
            var bodyContent = new StringContent(JsonSerializer.Serialize(this.RequestBody), Encoding.UTF8, "application/json");
            var response = await this.client.PostAsync(this.EndpointPath, bodyContent);

            this.LastResponseMessage = response;

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string path, object requestBody)
        {
            var bodyContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await this.client.PostAsync(path, bodyContent);

            return response;
        }

        public async Task<HttpResponseMessage> GetAsync()
        {
            var response = await this.client.GetAsync(this.EndpointPath);

            this.LastResponseMessage = response;

            return response;
        }

        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            var response = await this.client.GetAsync(path);

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync()
        {
            var bodyContent = new StringContent(JsonSerializer.Serialize(this.RequestBody), Encoding.UTF8, "application/json");
            var response = await this.client.PutAsync(this.EndpointPath, bodyContent);

            this.LastResponseMessage = response;

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync()
        {
            var response = await this.client.DeleteAsync(this.EndpointPath);

            this.LastResponseMessage = response;

            return response;
        }

        public async Task<T> ExtractResponseBodyAsync<T>()
        {
            var stringBody = await this.LastResponseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>
            (
                stringBody,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }
    }
}
