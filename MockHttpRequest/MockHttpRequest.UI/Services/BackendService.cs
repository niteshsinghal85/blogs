using Newtonsoft.Json;

namespace MockHttpRequest.UI.Services
{
	public class BackendService 
	{
		private readonly HttpClient _httpClient;

		public BackendService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<MyActivity> GetNewActity(CancellationToken cancellationToken = default)
		{
			var responseMessage = await _httpClient.GetAsync("/api/activity", cancellationToken);
			if(responseMessage.IsSuccessStatusCode)
			{
				var stream = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
				return JsonConvert.DeserializeObject<MyActivity>(stream);
			}
			return null;
		}
	}
}
