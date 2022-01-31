using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Repository;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone;
using TrelloClone.Services;

namespace TrelloCloneMVC
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddSession();
			services.AddHttpContextAccessor();
			var connectionString = Configuration["PostgreSql:ConnectionString"];
			var dbPassword = Configuration["PostgreSql:DbPassword"];

			var builder = new NpgsqlConnectionStringBuilder(connectionString)
			{
				Password = dbPassword
			};
			services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(builder.ConnectionString));

			services.AddSingleton(_ => Configuration["PostgreSql:ConnectionStringAdo"]);
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IBoardRepository, BoardRepository>();
			services.AddTransient<ICardListRepository, CardListRepository>();
			services.AddTransient<ICardRepository, CardRepository>();
			services.AddTransient<ICardLabelRepository, CardLabelRepository>();
			services.AddScoped<UserService>();
			services.AddScoped<BoardService>();
			services.AddScoped<CardListService>();
			services.AddScoped<CardService>();
			services.AddScoped<LabelService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseSession();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
