using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Security.Cryptography.X509Certificates;

namespace CertificateAuth;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.ConfigureKestrel(options =>
                {
                    options.ConfigureHttpsDefaults(o =>
                    {
                        o.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                    });
                });
            });
}