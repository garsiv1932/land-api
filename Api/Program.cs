using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseUrls("http://*:5000")
                        .UseStartup<Startup>();
                    // webBuilder.UseKestrel(e => e.ListenAnyIP(5000));
                    // webBuilder.UseStartup<Startup>();
                });

    }
}
