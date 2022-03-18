using Microsoft.AspNetCore.Mvc;
using MockHttpRequest.UI.Models;
using MockHttpRequest.UI.Services;
using System.Diagnostics;

namespace MockHttpRequest.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public async Task<IActionResult> Index([FromServices] BackendService backendService, CancellationToken cancellationToken)
		{
			var response = await backendService.GetNewActity(cancellationToken);

			if(response == null)
			{
				_logger.LogInformation("GetNewActivity : No response from backend");
				TempData["Error"] = "Error while fetching data";
				return View();
			}
			return View(response);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}