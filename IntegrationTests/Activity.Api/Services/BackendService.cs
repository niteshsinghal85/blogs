using Newtonsoft.Json;

namespace Activity.Api.Services
{
	public class BackendService : IBackendService
	{
		private readonly HttpClient _httpClient;

		public BackendService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<MyActivity?> GetNewActity(CancellationToken cancellationToken = default)
		{
			_httpClient.BaseAddress = new Uri("https://www.boredapi.com");
			var responseMessage = await _httpClient.GetAsync("/api/activity", cancellationToken);
			if (responseMessage.IsSuccessStatusCode)
			{
				var stream = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
				return JsonConvert.DeserializeObject<MyActivity>(stream);
			}
			return null;
		}
	}
}