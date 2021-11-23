using Microsoft.AspNetCore.Authentication.Certificate;
using System.Security.Cryptography.X509Certificates;

namespace CertificateAuth.Authentication
{
    public static class AuthenticationExtension
    {
        public static void ConfigureAuthetication(this IServiceCollection services)
        {
            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate(options =>
                {
                    options.RevocationMode = X509RevocationMode.NoCheck;
                    options.AllowedCertificateTypes = CertificateTypes.All;
                    options.Events = new CertificateAuthenticationEvents
                    {
                        OnCertificateValidated = context =>
                        {
                            var validationService = context.HttpContext.RequestServices.GetService<CertificateValidationService>();
                            if (validationService!= null && validationService.ValidateCertificate(context.ClientCertificate))
                            {
                                Console.WriteLine("Success");
                                context.Success();
                            }
                            else
                            {
                                Console.WriteLine("invalid cert 1");
                                context.Fail("invalid cert");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
        }
    }
}
