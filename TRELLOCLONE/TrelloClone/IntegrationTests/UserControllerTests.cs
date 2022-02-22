using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrelloClone;
using TrelloClone.Models;
using Xunit;

namespace IntegrationTests
{
	public class UserControllerTests : IClassFixture<AutofacWebApplicationFactory<Program>>
	{
		private readonly HttpClient _client;
		public UserControllerTests(AutofacWebApplicationFactory<Program> factory)
			=> _client = factory.CreateClient();

		[Fact]
		public async Task Get_existing_user_by_id_returns_200()
		{
			var response = await _client.GetAsync("/api/users/4");

			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var foundById = JsonConvert.DeserializeObject<User>(responseString);
			Console.WriteLine(responseString);
			foundById.Id.ShouldBe(4);
		}

		[Fact]
		public async Task Get_non_existing_user_by_id_returns_404()
		{
			var response = await _client.GetAsync("/api/users/0");
			var responseString = await response.Content.ReadAsStringAsync();
			var user = JsonConvert.DeserializeObject<User>(responseString);
			user.ShouldBeNull();
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task Get_user_count_returns_200()
		{
			var response = await _client.GetAsync("/api/users/count");
			var responseString = await response.Content.ReadAsStringAsync();
			var userCount = JsonConvert.DeserializeObject<Dictionary<string,int>>(responseString);
			userCount["userCount"].ShouldBeGreaterThanOrEqualTo(0);
			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public async Task Add_user_returns_200()
		{
			// Add User
			var user = new User()
			{
				Username = "TestUser1",
				Password = "TestPassword1",
				Role = "USER"
			};
			// Add User
			var requestJson = JsonConvert.SerializeObject(user);
			var request = new HttpRequestMessage(HttpMethod.Post, "/api/users");
			var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
			request.Content = stringContent;
			var responsePost = await _client.SendAsync(request);
			//responsePost.EnsureSuccessStatusCode();
			responsePost.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public async Task Modify_user_returns_200()
		{
			// Add User
			var user = new User()
			{
				Id = 4,
				Username = "TestUser1",
				Password = "TestPassword1",
				Role = "USER"
			};
			// Add User
			var requestJson = JsonConvert.SerializeObject(user);
			var request = new HttpRequestMessage(HttpMethod.Put, "/api/users");
			var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
			request.Content = stringContent;
			var responsePost = await _client.SendAsync(request);
			//responsePost.EnsureSuccessStatusCode();
			responsePost.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public async Task Find_user_by_username_returns_200()
		{
			// Find User
			var responseGet = await _client.GetAsync("/api/users/username/" + "MySpecialUsername1");
			//responseGet.EnsureSuccessStatusCode();
			responseGet.StatusCode.ShouldBe(HttpStatusCode.OK);
			var responseGetString = await responseGet.Content.ReadAsStringAsync();
			var userFromGetResponse = JsonConvert.DeserializeObject<User>(responseGetString);
			userFromGetResponse.Username.ShouldBe("MySpecialUsername1");
		}

		[Fact]
		public async Task Remove_user_by_id_returns_200()
		{
			var responseDelete = await _client.DeleteAsync("/api/users/" + 4);
			responseDelete.EnsureSuccessStatusCode();
			responseDelete.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

	}
}
