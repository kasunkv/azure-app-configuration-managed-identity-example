using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MusicStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, config) => {
                    var settings = config.Build();
                    var connectionString = settings["ConnectionStrings:AppConfiguration"];

                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        config.AddAzureAppConfiguration(options =>
                        {
                            options
                                .Connect(connectionString)
                                .UseFeatureFlags();
                        });
                    }
                });
    }
}
