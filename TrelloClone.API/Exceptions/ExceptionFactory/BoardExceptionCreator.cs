using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Exceptions.ExceptionFactory
{
	public class BoardExceptionCreator : ExceptionCreator
	{
		public override ICustomException CreateCustomException()
		{
			return new BoardNotFoundException();
		}
	}
}
