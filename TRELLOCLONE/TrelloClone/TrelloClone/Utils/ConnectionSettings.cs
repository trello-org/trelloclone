using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Utils
{
	public class ConnectionSettings
	{
		private readonly string _connectionString;

		public string ConnectionString { get { return _connectionString; } }

		public ConnectionSettings(IConfiguration configuration)
		{
			_connectionString = configuration["PostgreSql:ConnectionStringADO"];
		}
	}
}
