using Activity.Api.Models;
using Activity.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Activity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MyActivityController : ControllerBase
{
	private readonly ILogger<MyActivityController> _logger;
	private readonly IBackendService _backendService;

	public MyActivityController(ILogger<MyActivityController> logger,
		IBackendService backendService)
	{
		_logger = logger;
		_backendService = backendService;
	}

	[HttpGet(Name = "GetMyActivity")]
	public async Task<RandomActivity?> Get()
	{
		var res = await _backendService.GetNewActity();

		if (res != null)
		{
			return new RandomActivity()
			{
				Activity = res.Activity,
				Type = res.Type
			};
		}
		return null;
	}
}
