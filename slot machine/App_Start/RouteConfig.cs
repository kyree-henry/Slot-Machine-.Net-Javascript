using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;

namespace slot_machine.App_Start
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			var settings = new FriendlyUrlSettings();
			settings.AutoRedirectMode = RedirectMode.Off;
			routes.EnableFriendlyUrls(settings);
		}
	}
}