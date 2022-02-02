using Application.Middleware;
using Application.Services.Interfaces;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Npgsql;
using Repository;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Config;
using TrelloClone.Services;
using TrelloClone.Utils;

namespace TrelloClone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrelloClone", Version = "v1" });
            });

            /*
        
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
            services.AddScoped<LabelService>();*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrelloClone v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterModule(new AutoFacRootModule(Configuration));
        }
    }
}
