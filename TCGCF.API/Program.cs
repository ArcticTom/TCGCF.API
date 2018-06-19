using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TCGCF.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //build default webhost
            //CreateWebHostBuilder(args).UseStartup<Startup>().Build().Run();

            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .AddJsonFile("appSettings.json", true)
            .AddJsonFile("certificate.json", optional: true, reloadOnChange: true)
            .Build();

        var certificateSettings = config.GetSection("certificateSettings");
        string certificateFileName = certificateSettings.GetValue<string>("filename");
        string certificatePassword = certificateSettings.GetValue<string>("password");

        var certificate = new X509Certificate2(certificateFileName, certificatePassword);
        
        var host = new WebHostBuilder()
            .UseKestrel(
                options =>
                {
                    options.AddServerHeader = false;
                    options.Listen(IPAddress.Loopback, 44321, listenOptions =>
                    {
                        listenOptions.UseHttps(certificate);
                    });
                }
            )
            .UseConfiguration(config)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Startup>()
            .UseUrls("https://localhost:44321")
            .Build();

        host.Run();
        }

        //only used for entity framework tooling
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration((ctx, cfg) =>
            {
                cfg.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", true)
                .AddEnvironmentVariables();
            })
            .ConfigureLogging((ctx, logging) => { }) // No logging
            .UseStartup<Startup>();       
        }

    }
}
