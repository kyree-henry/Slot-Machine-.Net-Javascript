using System;
using System.IO;
using System.Web.Services;
using System.Web.UI;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Luckyme
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
        }

		
		[WebMethod]
		public static string LoadPage()
		{
			using (Page page = new Page())
			{
				UserControl userControl = (UserControl)page.LoadControl($"~/register.ascx");
				page.Controls.Add(userControl);
				using (StringWriter writer = new StringWriter())
				{
					page.Controls.Add(userControl);
					HttpContext.Current.Server.Execute(page, writer, false);
					return writer.ToString();
				}
			}
		}
		
	}
}