using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Http
{
	public class TokenClient
	{
		private readonly HttpClient _client;
		private readonly JsonSerializerOptions _options;

		public TokenClient(HttpClient client)
		{
			_client = client;

			_client.BaseAddress = new Uri("http://localhost:6177/api/");
			_client.Timeout = new TimeSpan(0, 0, 30);
			_client.DefaultRequestHeaders.Clear();

			_options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		public HttpClient Client { get { return _client; } }
		/*public async Task<List<CompanyDto>> GetCompanies()
		{
			using (var response = await _client.GetAsync("companies", HttpCompletionOption.ResponseHeadersRead))
			{
				response.EnsureSuccessStatusCode();

				var stream = await response.Content.ReadAsStreamAsync();

				var companies = await JsonSerializer.DeserializeAsync<List<CompanyDto>>(stream, _options);

				return companies;
			}
		}*/
	}
}
