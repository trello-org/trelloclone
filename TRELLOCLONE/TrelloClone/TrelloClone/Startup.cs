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
using Application.Services;
using TrelloClone.Utils;
using TrelloClone.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Http;

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

            /*builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                optionsBuilder.UseNpgsql(bd.ConnectionString);
                return new ApplicationContext(optionsBuilder.Options);
            }).InstancePerDependency();

             var connectionString = _configuration["PostgreSql:ConnectionString"];
            var dbPassword = _configuration["PostgreSql:DbPassword"];

            var bd = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Password = dbPassword
            };
            */

            var connectionString = Configuration["PostgreSql:ConnectionStringTest"];
            var dbPassword = Configuration["PostgreSql:DbPassword"];

            var bd = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Password = dbPassword
            };

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(bd.ConnectionString);
            });

            services.AddMemoryCache();
            //services.AddHttpClient();
            /* services.AddHttpClient("TokenClient", config =>
             {
                 config.BaseAddress = new Uri("https://localhost:6177/api/");
                 config.Timeout = new TimeSpan(0, 0, 30);
                 config.DefaultRequestHeaders.Clear();
             });*/
            services.AddHttpClient<TokenClient>();
            services.AddScoped<IHttpClientServiceImplementation, HttpClientFactoryService>();
            var key = Encoding.ASCII.GetBytes("mylittlesecretkeyneedstobelongenough");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            
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
            app.UseAuthentication();

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();

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
