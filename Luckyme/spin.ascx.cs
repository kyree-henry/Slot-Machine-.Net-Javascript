using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Luckyme
{
	public partial class spin : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string connectionstring = WebConfigurationManager.ConnectionStrings["aspnet-lucyme"].ConnectionString;
			SqlConnection connection = new SqlConnection(connectionstring);

			try
			{
				connection.Open();
				SqlCommand sqlCommand = connection.CreateCommand();


			}catch (Exception ex)
			{

			}
		}

		protected void Logout(object sender, EventArgs e)
		{
			Session.Clear();
		}


	}
}