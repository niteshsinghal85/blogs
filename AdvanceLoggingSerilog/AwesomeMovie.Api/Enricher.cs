using Serilog;
using System.Security.Claims;

namespace awesomemovie.Api
{
	public class HttpContextInfo
	{
		public string IpAddress { get; set; }
		public string Host { get; set; }
		public string Protocol { get; set; }
		public string Scheme { get; set; }
		public string User { get; set; }
	}

	public static class Enricher
	{
		internal static void HttpRequestEnricher(IDiagnosticContext diagnosticContext, HttpContext httpContext)
		{
			var httpContextInfo = new HttpContextInfo
			{
				Protocol = httpContext.Request.Protocol,
				Scheme = httpContext.Request.Scheme,
				IpAddress = httpContext.Connection.RemoteIpAddress.ToString(),
				Host = httpContext.Request.Host.ToString(),
				User = GetUserInfo(httpContext.User)
			};

			diagnosticContext.Set("HttpContext", httpContextInfo, true);
		}

		private static string GetUserInfo(ClaimsPrincipal user)
		{
			if(user.Identity != null && user.Identity.IsAuthenticated)
			{
				return user.Identity.Name;
			}
			return Environment.UserName;
		}
	}
}
