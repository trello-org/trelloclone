using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;
using Xunit;

namespace TrelloCloneTests.IntegrationTests
{
	public class UserControllerTests : IClassFixture<TestingWebAppFactory<Program>>
	{
		private readonly HttpClient _client;
		public UserControllerTests(TestingWebAppFactory<Program> factory)
			=> _client = factory.CreateClient();

		
		public async Task Index_WhenCalled_ReturnsApplicationForm()
		{
			var response = await _client.GetAsync("/api/users/4");

			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var user = JsonConvert.DeserializeObject<User>(responseString);

			user.Id.ShouldBe(4);
		}
	}
}
