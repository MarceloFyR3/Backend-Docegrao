using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NetHacksPack.Hosting ;

namespace DoceGrao.Api.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(configuration =>
                {
                    configuration.SetBasePath(Directory.GetCurrentDirectory());

                    var environment = System.Environment.GetEnvironmentVariable($"ASPNETCORE_ENVIRONMENT");
                    configuration
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile(string.Format("appsettings.{0}.json", environment), optional: true, reloadOnChange: true)
                        .AddJsonFile("appsettings.database.json", optional: true, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
