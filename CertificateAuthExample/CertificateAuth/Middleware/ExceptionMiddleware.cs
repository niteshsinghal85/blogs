using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace CertificateAuth.Middleware;

public static class ExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                // setting the response code as internal server error.
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                if (contextFeature != null)
                {
                    var ex = contextFeature?.Error;
                    var isDev = env.IsDevelopment();
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(
                        // using problem details object to  response to caller
                        new ProblemDetails
                        {
                            Type = ex?.GetType().Name,
                            Status = (int)HttpStatusCode.InternalServerError,
                            Instance = contextFeature?.Path,
                                // i am just using generic statement.
                                // it can be customised based on path or any other condition
                                Title = isDev ? $"{ex?.Message}" : "An error occurred.",
                                // in case of dev, it returns the complete stack trace.
                                Detail = isDev ? ex?.StackTrace : null
                        }));
                }
            });
        });
    }
}
