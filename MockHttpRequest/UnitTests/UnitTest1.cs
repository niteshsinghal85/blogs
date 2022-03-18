using NUnit.Framework;
using Moq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Moq.Protected;
using System.Threading;
using MockHttpRequest.UI.Services;

namespace UnitTests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task TestBackendServiceAsync()
		{
			var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

			HttpResponseMessage httpResponseMessage = new()
			{
				Content = JsonContent.Create(new
				{
					activity = "Listen to music",
					type = "music"
				})
			};

			// Set up the SendAsync method behavior.
			httpMessageHandlerMock
				.Protected() // <= this is most important part that it need to setup.
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",ItExpr.IsAny<HttpRequestMessage>(),ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(httpResponseMessage);

			// create the HttpClient
			var httpClient = new HttpClient(httpMessageHandlerMock.Object)
			{
				BaseAddress = new System.Uri("http://localhost") // It should be in valid uri format.
			};

			BackendService backendService = new(httpClient);
			//Act
			var result = await backendService.GetNewActity();
			//Assert
			Assert.AreEqual("Listen to music", result.Activity);
		}
	}
}