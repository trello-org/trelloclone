using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityTypeConfigurations
{
	public class ConnectionStrings
	{
		private readonly string _connectionString;

		public string ConnectionString { get { return _connectionString; } }

		public ConnectionStrings(IConfiguration configuration)
		{
			_connectionString = configuration["PostgreSql:ConnectionStringADO"];
		}
	}
}
