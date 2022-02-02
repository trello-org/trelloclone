using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Exceptions.ExceptionFactory
{
	public static class ExceptionThrower
	{
		public static void ThrowException(ExceptionCreator exceptionCreator)
		{
			var ex = exceptionCreator.LogException();
			throw (Exception)ex;
		}
	}
}
