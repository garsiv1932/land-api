using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.SRVs;

namespace Api
{
    public class Program
    {
        private readonly IConfiguration _configuration;

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            createDbIfNosExists(host);
            host.Run();
        }

        public Program(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static void createDbIfNosExists(IHost host)
        {
            using (var scope= host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApiContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // webBuilder.UseKestrel()
                    //     .UseContentRoot(Directory.GetCurrentDirectory())
                    //     .UseUrls("http://*:5000")
                    //     .UseStartup<Startup>();
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(e => e.ListenAnyIP(5000));
                });
    }
}
