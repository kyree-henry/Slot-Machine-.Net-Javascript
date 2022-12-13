using System;
using System.Web;
using System.Web.UI;

namespace Luckyme.App_Code
{
	public class jQueryHandler : IHttpHandler
	{
		public bool IsReusable
		{
			get { throw new NotImplementedException(); }
		}

		public void ProcessRequest(HttpContext context)
		{
			using (Page dummyPage = new Page())
			{
				dummyPage.Controls.Add(GetControl(context));
				context.Server.Execute(dummyPage, context.Response.Output, true);
			}
		}
		private Control GetControl(HttpContext context)
		{
			// URL path given by load(fn) method on click of button
			string strPath = context.Request.Url.LocalPath;
			UserControl userctrl = null;
			using (Page dummyPage = new Page())
			{
				userctrl = dummyPage.LoadControl(strPath) as UserControl;
			}
			// Loaded user control is returned
			return userctrl;
		}
	}
}