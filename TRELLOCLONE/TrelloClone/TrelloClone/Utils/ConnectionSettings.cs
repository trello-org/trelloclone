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
		private readonly string _connectionStringTest;
		public string ConnectionString { get { return _connectionString; } }
		public string ConnectionStringTest {  get { return _connectionStringTest; } }

		public ConnectionSettings(IConfiguration configuration)
		{
			_connectionString = configuration["PostgreSql:ConnectionStringADO"];
			_connectionStringTest = configuration["PostgreSql:ConnectionStringTest"];
		}
	}
}
