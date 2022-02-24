using Application.Dtos;
using Application.Http;
using Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HttpClientFactoryService : IHttpClientServiceImplementation
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TokenClient _tokenClient;
        private readonly JsonSerializerOptions _options;
        public HttpClientFactoryService(IHttpClientFactory httpClientFactory, TokenClient tokenClient)
        {
            _httpClientFactory = httpClientFactory;
            _tokenClient = tokenClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<JwtToken> RefreshToken(string token)
		{

            using (var response = await _tokenClient.Client.SendAsync(RefreshRequestMessage(token)))
            {
				Console.WriteLine(response.RequestMessage + response.StatusCode.ToString() + " " + response.Content);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var ret = await JsonSerializer.DeserializeAsync<JwtToken>(stream, _options);
				Console.WriteLine("Token successfully deserialized");
                return ret;
            }
        }

        private HttpRequestMessage RefreshRequestMessage(string token)
		{
            var request = new HttpRequestMessage(HttpMethod.Post, "users/refresh-token");
			Console.WriteLine($"Adding Cookie header     {token}");
            request.Headers.Add("Cookie", $"refreshToken={token};");
            
            return request;
        }
    }
}
