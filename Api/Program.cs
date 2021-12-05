using System.IO;
using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
                    
                    // webBuilder.ConfigureKestrel(options => {               
                    //     var port = 5001;
                    //     var pfxFilePath = @"./certs/llorachdevs/https.pfx";
                    //     // The password you specified when exporting the PFX file using OpenSSL.
                    //     // This would normally be stored in configuration or an environment variable;
                    //     // I've hard-coded it here just to make it easier to see what's going on.
                    //     var pfxPassword = "03111984"; 
                    //
                    //     options.Listen(IPAddress.Any, 5000);
                    //     options.Listen(IPAddress.Any, port, listenOptions => {
                    //         // Enable support for HTTP1 and HTTP2 (required if you want to host gRPC endpoints)
                    //         // listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    //         // Configure Kestrel to use a certificate from a local .PFX file for hosting HTTPS
                    //         listenOptions.UseHttps(pfxFilePath, pfxPassword);
                    //     });
                    // });

                    // webBuilder.UseKestrel()
                    //     .UseStartup<Startup>();
                    
                    
                    webBuilder.UseKestrel()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseUrls("http://*:5000")
                        .UseStartup<Startup>();
                    
                    // webBuilder.UseStartup<Startup>()
                    //     .UseKestrel((context, serverOptions) =>
                    //     {
                    //         serverOptions.Configure(context.Configuration.GetSection("Kestrel"))
                    //             .Endpoint("HTTPS", listenOptions =>
                    //             {
                    //                 listenOptions.HttpsOptions.SslProtocols = SslProtocols.Tls12;
                    //             });
                    //     });
                    //
                    
                    // webBuilder.ConfigureKestrel(o =>
                    // {
                    //     o.ConfigureHttpsDefaults(o => 
                    //         o.ClientCertificateMode = Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.RequireCertificate);
                    // });
                    // webBuilder.UseKestrel(e => e.ListenAnyIP(5000));
                    // webBuilder.UseStartup<Startup>();
                });
    }
}
