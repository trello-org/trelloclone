using Application.Services;
using Autofac.Extras.Moq;
using Moq;
using Repository.Repository;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloClone.Models;
using Xunit;

namespace TrelloCloneTests
{
	public class UserServiceTests
	{
		/*private readonly Mock<IUserRepository> _mockRepo;

		public UserServiceTests()
		{
			_mockRepo = new Mock<IUserRepository>();
		}*/

		[Fact]
		public async Task UserService_GetAllUsersAsync()
		{
			var retList = new List<User>(){
				new User(), 
				new User()
			};

			using (var mock = AutoMock.GetLoose())
			{
				mock.Mock<IUserRepository>().Setup(x => x.GetAllAsync()).Returns(Task.FromResult(retList));
				var userService = mock.Create<UserService>();

				var assertOnList = await userService.GetAllAsync();
				assertOnList.ShouldBe(retList);
			}
		}

		[Fact]
		public async Task UserService_AddAsync_ShouldThrowException_InvalidFields()
		{
			var user = new User();
			user.Username = "";
			user.Password = "";

			using (var mock = AutoMock.GetLoose())
			{
				var repo = mock.Mock<IUserRepository>();
				var userService = mock.Create<UserService>();
				await userService.AddAsync(user);
				repo.Verify(x => x.AddAsync(user), Times.Once());
				var exception = await Should.ThrowAsync<Exception>(async () => await userService.AddAsync(user));
			}
		}

		[Fact]
		public async Task UserService_CountUsersAsync()
		{
			var retList = new List<User>(){
				new User(),
				new User()
			};

			using (var mock = AutoMock.GetLoose())
			{
				mock.Mock<IUserRepository>().Setup(x => x.CountUsersAsync()).Returns(Task.FromResult(retList.Count));
				var userService = mock.Create<UserService>();

				var userCount = await userService.CountUsersAsync();
				userCount.ShouldBe(2);
			}
		}

		[Fact]
		public async Task UserService_FindAsync()
		{
			Expression<Func<User, bool>> condition = u => u.Username.Length < 8;
			var user1 = new User() { Username = "longusername1" };
			var user2 = new User() { Username = "user" };
			var inputList = new List<User>() { user1, user2 };

			var retList = new List<User>() { user2 };
			using (var mock = AutoMock.GetLoose())
			{
				mock.Mock<IUserRepository>().Setup(x => x.FindAsync(condition)).Returns(Task.FromResult(retList));
				var userService = mock.Create<UserService>();

				var foundUsers = await userService.FindAsync(condition);
				foundUsers.ShouldBe(retList);
			}
		}

		[Fact]
		public async Task UserService_FindByUsernameAsync()
		{
			var user2 = new User() { Username = "user" };
			using (var mock = AutoMock.GetLoose())
			{
				mock.Mock<IUserRepository>().Setup(x => x.FindByUsernameAsync("user")).Returns(Task.FromResult(user2));
				var userService = mock.Create<UserService>();

				var foundUser = await userService.FindByUsernameAsync("user");
				foundUser.Username.ShouldBe("user");
			}
		}

		[Fact]
		public async Task UserService_GetByIdAsync()
		{
			var user2 = new User() { Id = 1337 };

			using (var mock = AutoMock.GetLoose())
			{
				mock.Mock<IUserRepository>().Setup(x => x.GetByIdAsync(1337)).Returns(Task.FromResult(user2));
				var userService = mock.Create<UserService>();

				var foundUser = await userService.GetByIdAsync(1337);
				foundUser.Id.ShouldBe(1337);
			}
		}

		[Fact]
		public async Task UserService_RemoveAsync()
		{
			var user = new User();
			user.Id = 1;

			using (var mock = AutoMock.GetLoose())
			{
				var repo = mock.Mock<IUserRepository>();
				var userService = mock.Create<UserService>();
				await userService.RemoveAsync(1);
				repo.Verify(x => x.RemoveAsync(1), Times.Once());
			}
		}

		[Fact]
		public async Task UserService_UpdateAsync()
		{
			var user = new User();
			user.Id = 1;
			user.Username = "username1";

			using (var mock = AutoMock.GetLoose())
			{
				var repo = mock.Mock<IUserRepository>();
				var userService = mock.Create<UserService>();
				await userService.UpdateAsync(user);
				repo.Verify(x => x.UpdateAsync(user), Times.Once());
			}
		}



	}
}
