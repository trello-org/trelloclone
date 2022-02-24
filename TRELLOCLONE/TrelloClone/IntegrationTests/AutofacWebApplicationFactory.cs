using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace IntegrationTests
{
    public class AutofacWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseServiceProviderFactory<ContainerBuilder>(new CustomServiceProviderFactory());
            return base.CreateHost(builder);
        }
    }

    public class CustomServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        private AutofacServiceProviderFactory _wrapped;
        private IServiceCollection _services;

        public CustomServiceProviderFactory()
        {
            _wrapped = new AutofacServiceProviderFactory();
        }

        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDb");
            });
            // Store the services for later.
            _services = services;

            return _wrapped.CreateBuilder(services);
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            var sp = _services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>())
            {
                try
                {
                    appContext.Database.EnsureCreated();
                    appContext.Users.Add(new User()
                    {                
                        Username = "MySpecialUsername1",
                        Password = "MySpecialPassword1"
                    });
                    appContext.Users.Add(new User()
                    {
                        Username = "MySpecialUsername2",
                        Password = "MySpecialPassword2"
                    });

                    appContext.Users.Add(new User()
                    {
                        Username = "MySpecialUsername3",
                        Password = "MySpecialPassword3"
                    });


                    appContext.Boards.Add(new Board()
                    {
                        Name = "MyNewboard",
                        Description = "My new board.",
                        IsPublic = true,
                        UserId = 1116
                    });

                    appContext.Boards.Add(new Board()
                    {
                        Name = "MyNewboardyy",
                        Description = "My new boardyy",
                        IsPublic = true,
                        UserId = 1117
                    });

                    appContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            }
#pragma warning disable CS0612 // Type or member is obsolete
            var filters = sp.GetRequiredService<IEnumerable<IStartupConfigureContainerFilter<ContainerBuilder>>>();
#pragma warning restore CS0612 // Type or member is obsolete

            foreach (var filter in filters)
            {
                filter.ConfigureContainer(b => { })(containerBuilder);
            }

            return _wrapped.CreateServiceProvider(containerBuilder);
        }
    }
}
