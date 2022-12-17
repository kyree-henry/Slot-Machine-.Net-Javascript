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

		protected void GotoLogin(object sender, EventArgs e)
		{
			Response.Redirect("default.aspx");
		}

		protected void SubmitForm(object sender, EventArgs e)
		{
			if (password.Text != confirmPassword.Text)
			{
				error.IsValid = false;
				error.ErrorMessage = "Passwords do not Match";
				return;
			}

			string connectionString = WebConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				SqlCommand query = new SqlCommand($"select * from tbl_players where username='{username.Text}'", connection);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
				DataTable result = new DataTable();
				sqlDataAdapter.Fill(result);
				connection.Open();

				int i = query.ExecuteNonQuery();

				if (result.Rows.Count > 0)
				{
					error.IsValid = false;
					error.ErrorMessage = "User already Exsits!";
				}
				else
				{
					query = new SqlCommand($"insert into tbl_players (username, password, credits, wins) values ('{username.Text}','{password.Text}', 5, 0)", connection);
					sqlDataAdapter = new SqlDataAdapter(query);

					i = query.ExecuteNonQuery();

					if (i > 0) Response.Redirect("default.aspx");
					else
					{
						error.IsValid = false;
						error.ErrorMessage = "Data not inserted!";

					}
				}

			}
			catch (SqlException ex)
			{
				error.IsValid = false;
				error.ErrorMessage = ex.Message;
			}
			catch (Exception ex)
			{
				error.IsValid = false;
				error.ErrorMessage = ex.Message;
			}
		}

	}
}