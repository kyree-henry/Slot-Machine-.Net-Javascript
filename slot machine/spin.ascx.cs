using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace slot_machine
{
	public partial class spin : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string connectionString = WebConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				SqlCommand query = new SqlCommand($"select * from tbl_players where id='{Session["userId"]}'", connection);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
				DataTable result = new DataTable();
				sqlDataAdapter.Fill(result);
				connection.Open();

				int i = query.ExecuteNonQuery();
				connection.Close();

				if (result.Rows.Count == 1)
				{
					foreach (DataRow data in result.Rows)
					{
						credits.Text = data["credits"].ToString();
						wins.Text = data["wins"].ToString();
					}
				}

			}
			catch (Exception)
			{ }

		}

		protected void Logout(object sender, EventArgs e)
		{
			Session.Clear();
		}

		protected void AddCredit(object sender, EventArgs e)
		{
			string connectionString = WebConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				SqlCommand query = new SqlCommand($"select * from tbl_players where id='{Session["userId"]}'", connection);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
				DataTable result = new DataTable();
				sqlDataAdapter.Fill(result);
				connection.Open();

				int i = query.ExecuteNonQuery();

				if (result.Rows.Count == 1)
				{
					int newCreditValue = 0;
					foreach (DataRow data in result.Rows)
					{
						newCreditValue = Convert.ToInt32(data["credits"].ToString());
					}

					newCreditValue += 5;

					query = new SqlCommand($"update tbl_players set credits = '{newCreditValue}' where id='{Session["userId"]}'", connection);
					sqlDataAdapter = new SqlDataAdapter(query);

					i = query.ExecuteNonQuery();

					if (i > 0) Response.Redirect("default.aspx");

				}
				connection.Close();
			}
			catch (Exception ex)
			{

			}
		}

	}
}