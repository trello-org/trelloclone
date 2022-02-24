using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                logger.Debug("Hellou");
                //CreateHostBuilder(args).Build().Run();
                // ASP.NET Core 3.0+:
                // The UseServiceProviderFactory call attaches the
                // Autofac provider to the generic hosting mechanism.
                var host = Host.CreateDefaultBuilder(args)
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureWebHostDefaults(webHostBuilder => {
                        webHostBuilder
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseIISIntegration()
              .UseStartup<Startup>();
                    })
                    .Build();

                host.Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();  // NLog: Setup NLog for Dependency injection;
    }

   // public partial class Program { }
}
