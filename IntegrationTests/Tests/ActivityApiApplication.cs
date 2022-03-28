using Activity.Api.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Moq.Protected;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Activity.Api.Tests
{
	internal class ActivityApiApplication : WebApplicationFactory<Program>
	{
		protected override IHost CreateHost(IHostBuilder builder)
		{
			var mockHttpClient = GetMockedHttpClient();
			
			builder.ConfigureServices(services =>
			{
				ServiceDescriptor serviceDescriptor = new(typeof(IBackendService),
					typeof(BackendService), ServiceLifetime.Scoped);
				services.Remove(serviceDescriptor);
				services.AddScoped<IBackendService>(s => new BackendService(mockHttpClient));
			});

			return base.CreateHost(builder);
		}

		private HttpClient GetMockedHttpClient()
		{
			HttpResponseMessage httpResponseMessage = new()
			{
				StatusCode = System.Net.HttpStatusCode.InternalServerError
			};

			var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
			// Set up the SendAsync method behavior.
			httpMessageHandlerMock
				.Protected() // <= this is most important part that it need to setup.
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(httpResponseMessage);

			// create the HttpClient
			var httpClient = new HttpClient(httpMessageHandlerMock.Object)
			{
				BaseAddress = new System.Uri("http://localhost") // It should be in valid uri format.
			};

			return httpClient;
		}
	}
}
