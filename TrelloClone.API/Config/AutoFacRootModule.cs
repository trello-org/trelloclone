using Application.Middleware;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Repository;
using Repository.EntityTypeConfigurations;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using TrelloClone.Filters;
using Application.Services;
using TrelloClone.Utils;

namespace TrelloClone.Config
{
	public class AutoFacRootModule : Autofac.Module
	{
		private IConfiguration _configuration;

		public AutoFacRootModule(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		protected override void Load(ContainerBuilder builder)
        {
            var connectionString = _configuration["PostgreSql:ConnectionString"];
            var dbPassword = _configuration["PostgreSql:DbPassword"];

            var bd = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Password = dbPassword
            };

            builder.Register(cs => { return new ConnectionStrings(_configuration); }).AsSelf().SingleInstance();

            
            builder.RegisterType<ConnectionSettings>().AsSelf().InstancePerLifetimeScope();
           
            /*builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                optionsBuilder.UseNpgsql(bd.ConnectionString);
                return new ApplicationContext(optionsBuilder.Options);
            }).InstancePerDependency();
*/

            // convention projectName.
            builder.RegisterAssemblyTypes(Assembly.Load("Repository"))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.Load("Application"))
                .Where(t => t.Name.EndsWith("Service"))
                .InstancePerLifetimeScope();
		}
    }
}
