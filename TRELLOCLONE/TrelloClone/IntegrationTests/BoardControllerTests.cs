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
	public class BoardControllerTests : IClassFixture<AutofacWebApplicationFactory<Program>>
	{
		private readonly HttpClient _client;
		public BoardControllerTests(AutofacWebApplicationFactory<Program> factory)
			=> _client = factory.CreateClient();

		[Fact]
		public async Task Get_existing_board_by_id_returns_200()
		{
			var response = await _client.GetAsync("/api/boards/2221");

			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var foundById = JsonConvert.DeserializeObject<Board>(responseString);
			Console.WriteLine(responseString);
			foundById.Id.ShouldBe(1111);
		}

		[Fact]
		public async Task Get__non_existing_board_by_id_returns_404()
		{
			var response = await _client.GetAsync("/api/boards/1337");

			var responseString = await response.Content.ReadAsStringAsync();
			var foundById = JsonConvert.DeserializeObject<Board>(responseString);
			Console.WriteLine(responseString);
			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task Add_board_returns_200()
		{
			// Add Board
			var board = new Board()
			{
				Name = "MyNewestBoard",
				Description = "MyNewestDescription1",
				IsPublic = true,
				UserId = 1114
			};
			// Add Board
			var requestJson = JsonConvert.SerializeObject(board);
			var request = new HttpRequestMessage(HttpMethod.Post, "/api/boards");
			var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
			request.Content = stringContent;
			var responsePost = await _client.SendAsync(request);
			//responsePost.EnsureSuccessStatusCode();
			responsePost.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public async Task Modify_board_returns_200()
		{
			// Add Board
			var board = new Board()
			{
				Id = 2221,
				Name = "MyNewestBoard",
				Description = "MyNewestDescription1",
				IsPublic = true,
				UserId = 1114
			};
			// Add Board
			var requestJson = JsonConvert.SerializeObject(board);
			var request = new HttpRequestMessage(HttpMethod.Put, "/api/boards");
			var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
			request.Content = stringContent;
			var responsePost = await _client.SendAsync(request);
			//responsePost.EnsureSuccessStatusCode();
			responsePost.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public async Task Modify_non_existing_board_returns_400()
		{
			// Add Board
			var board = new Board()
			{
				Id = 1337,
				Name = "MyNewestBoard",
				Description = "MyNewestDescription1",
				IsPublic = true,
				UserId = 5
			};
			// Add Board
			var requestJson = JsonConvert.SerializeObject(board);
			var request = new HttpRequestMessage(HttpMethod.Put, "/api/boards");
			var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
			request.Content = stringContent;
			var responsePost = await _client.SendAsync(request);
			//responsePost.EnsureSuccessStatusCode();
			//responsePost.ReasonPhrase.ShouldBe("");
			responsePost.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task Remove_board_by_id_returns_200()
		{
			var responseDelete = await _client.DeleteAsync("/api/boards/2222");
			responseDelete.EnsureSuccessStatusCode();
			responseDelete.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public async Task Get_all_boards_for_user_returns_200()
		{
			var response = await _client.GetAsync("/api/boards/users/1114");

			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			var boardList = JsonConvert.DeserializeObject<List<Board>>(responseString);
			Console.WriteLine(responseString);
			boardList.Count.ShouldBeGreaterThanOrEqualTo(0);
		}
	}
}
