using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace slot_machine
{
	public partial class register : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Session.Clear();
		}
	}
}