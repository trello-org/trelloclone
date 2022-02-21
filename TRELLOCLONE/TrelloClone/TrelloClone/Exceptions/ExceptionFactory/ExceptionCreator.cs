using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Exceptions.ExceptionFactory
{
	public abstract class ExceptionCreator
	{
		public abstract ICustomException CreateCustomException();

		public ICustomException LogException()
		{
			var customException = CreateCustomException();
			customException.LogException();
			return customException;
		}

	}
}
