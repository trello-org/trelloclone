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
			// Add User
			var user = new User()
			{
				Username = "TestUser1",
				Password = "TestPassword1",
				Role = "USER"
			};
			await Add_user_should_return_200(user);

			// Find User
			var found = await Find_user_by_username_should_return_200(user.Username);

			var response = await _client.GetAsync("/api/users/" + found.Id);

			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var foundById = JsonConvert.DeserializeObject<User>(responseString);
			Console.WriteLine(responseString);
			foundById.Id.ShouldBe(found.Id);

			// Delete User

			await Remove_user_by_id_should_return_200(found.Id);
		
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
		public async Task Add_user_and_then_remove_them()
		{
			// Add User
			var user = new User()
			{
				Username = "TestUser1",
				Password = "TestPassword1",
				Role = "USER"
			};
			await Add_user_should_return_200(user);

			// Find User
			var found = await Find_user_by_username_should_return_200(user.Username);

			// Delete User

			await Remove_user_by_id_should_return_200(found.Id);
		}

		[Fact]
		public async Task Add_user_modify_them_and_then_remove_them()
		{
			// Add User
			var user = new User()
			{
				Username = "TestUser1",
				Password = "TestPassword1",
				Role = "USER"
			};
			await Add_user_should_return_200(user);

			// Find User 

			var found = await Find_user_by_username_should_return_200(user.Username);

			found.Username = "NewTestUsername1";
			await Modify_user_should_return_200(found);

			var foundAfterModification = await Find_user_by_username_should_return_200(found.Username);

			foundAfterModification.Username.ShouldNotBe(user.Username);

			await Remove_user_by_id_should_return_200(foundAfterModification.Id);
		}

		internal async Task Add_user_should_return_200(User user)
		{
			// Add User
			var requestJson = JsonConvert.SerializeObject(user);
			var request = new HttpRequestMessage(HttpMethod.Post, "/api/users");
			var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
			request.Content = stringContent;
			var responsePost = await _client.SendAsync(request);
			//responsePost.EnsureSuccessStatusCode();
			responsePost.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		internal async Task Modify_user_should_return_200(User user)
		{
			// Add User
			var requestJson = JsonConvert.SerializeObject(user);
			var request = new HttpRequestMessage(HttpMethod.Put, "/api/users");
			var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
			request.Content = stringContent;
			var responsePost = await _client.SendAsync(request);
			//responsePost.EnsureSuccessStatusCode();
			responsePost.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		public async Task<User> Find_user_by_username_should_return_200(string username)
		{
			// Find User
			var responseGet = await _client.GetAsync("/api/users/username/" + username);
			//responseGet.EnsureSuccessStatusCode();
			responseGet.StatusCode.ShouldBe(HttpStatusCode.OK);
			var responseGetString = await responseGet.Content.ReadAsStringAsync();
			var userFromGetResponse = JsonConvert.DeserializeObject<User>(responseGetString);
			return userFromGetResponse;
		}

		internal async Task Remove_user_by_id_should_return_200(long id) {
			// Delete User
			var responseDelete = await _client.DeleteAsync("/api/users/" + id);
			//responseDelete.EnsureSuccessStatusCode();
			responseDelete.StatusCode.ShouldBe(HttpStatusCode.OK);
		}
	}
}
