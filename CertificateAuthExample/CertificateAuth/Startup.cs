using CertificateAuth.Authentication;
using CertificateAuth.Middleware;
using Microsoft.OpenApi.Models;

namespace CertificateAuth;

public class Startup
{
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<CertificateValidationService>();
            services.ConfigureAuthetication();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CertificateAuth", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureExceptionHandler(env);
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CertificateAuth v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
}