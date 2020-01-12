using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).
            //ADDED LOGGING FOR DEBUG WINDOW WITH MINIMUM SET TO WARNING.
            ConfigureLogging(loggingConfig=>
            {
                loggingConfig.ClearProviders();
                loggingConfig.AddDebug();
                loggingConfig.SetMinimumLevel(LogLevel.Warning);

            }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
