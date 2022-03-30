using awesomemovie.Api;
using AwesomeMovie.Data;
using Serilog;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// serilog configuration
builder.Host.UseSerilog((context, loggerConfig) => {
    loggerConfig
    .ReadFrom.Configuration(context.Configuration)
    .WriteTo.Console()
    .Enrich.WithExceptionDetails()
    .Enrich.FromLogContext()
    .WriteTo.Seq("http://localhost:5341");
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// database
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();


var app = builder.Build();

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = Enricher.HttpRequestEnricher;
});


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.MigrateAndCreateData();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseMiddleware<UserInfoMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
