using System.IO;
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
            WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>()
            .Build()
            .Run();

        }

        //only used for entity framework tooling
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration((ctx, cfg) =>
            {
                cfg.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", true)
                .AddEnvironmentVariables();
            })
            .ConfigureLogging((ctx, logging) => { }) // No logging
            .UseStartup<Startup>()
            .UseSetting("DesignTime", "true") //set DesignTime to true
            .Build();
        }

    }
}
