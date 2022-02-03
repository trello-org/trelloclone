using Application.Services;
using Autofac.Extras.Moq;
using Moq;
using Repository.Repository;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;
using Xunit;

namespace TrelloCloneTests
{
	public class BoardServiceTests
	{
		[Fact]
		public async Task BoardService_AddAsync()
		{
			var board = new Board();

			using (var mock = AutoMock.GetLoose())
			{
				var repo = mock.Mock<IBoardRepository>();
				var boardService = mock.Create<BoardService>();
				await boardService.AddAsync(board);
				repo.Verify(x => x.AddAsync(board), Times.Once());
			}
		}

		[Fact]
		public async Task BoardService_GetAllBoardsForUser()
		{
			var retList = new List<Board>() { new Board(), new Board() };

			using (var mock = AutoMock.GetLoose())
			{
				mock.Mock<IBoardRepository>().Setup(x => x.GetAllBoardsForUserAsync(1)).Returns(Task.FromResult(retList));
				var boardService = mock.Create<BoardService>();

				var assertOnList = await boardService.GetAllBoardsForUserAsync(1);
				assertOnList.ShouldBe(retList);
			}
		}

		[Fact]
		public async Task BoardService_EditBoardVisibilityAsync()
		{
			var board = new Board();

			using (var mock = AutoMock.GetLoose())
			{
				var repo = mock.Mock<IBoardRepository>();
				var boardService = mock.Create<BoardService>();
				await boardService.EditBoardVisibilityAsync(board);
				repo.Verify(x => x.EditBoardVisibilityAsync(board), Times.Once());
			}
		}

		[Fact]
		public async Task BoardService_GetByIdAsync()
		{
			var retBoard = new Board() { Id = 1337 };

			using (var mock = AutoMock.GetLoose())
			{
				mock.Mock<IBoardRepository>().Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult(retBoard));
				var boardService = mock.Create<BoardService>();

				var foundBoard = await boardService.GetByIdAsync(1);
				foundBoard.ShouldBe(retBoard);
			}
		}

		[Fact]
		public async Task BoardService_GetAllAsync()
		{
			var retList = new List<Board>() { new Board(), new Board() };

			using (var mock = AutoMock.GetLoose())
			{
				mock.Mock<IBoardRepository>().Setup(x => x.GetAllAsync()).Returns(Task.FromResult(retList));
				var boardService = mock.Create<BoardService>();

				var assertOnList = await boardService.GetAllAsync();
				assertOnList.Count.ShouldBe(2);
			}
		}
	}


}
