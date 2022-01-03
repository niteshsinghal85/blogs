using Microsoft.FeatureManagement;
using Winton.Extensions.Configuration.Consul;

var builder = WebApplication.CreateBuilder(args);


builder.Host.ConfigureAppConfiguration((context, config) => 
{
	var consulHost = config.Build().GetValue<string>("ConsulServer");
	config.AddConsul("FeatureManagement", options =>
	{
		options.ConsulConfigurationOptions = cco =>
		{
			cco.Address = new System.Uri(consulHost);
			cco.Token = "d5da071e-1ff4-7af3-0863-dd8294611740";
		};
		options.Optional = false;
		options.ReloadOnChange = true;
		options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
	});
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFeatureManagement();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
