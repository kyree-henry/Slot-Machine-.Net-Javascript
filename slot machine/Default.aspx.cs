using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web;

namespace slot_machine
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{ }

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

		[WebMethod]
		public static string Payout(string slot1value, string slot2value, string slot3value, int betcredit)
		{
			string connectionString = WebConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				SqlCommand query = new SqlCommand($"select * from tbl_players where id='{HttpContext.Current.Session["userId"]}'", connection);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
				DataTable result = new DataTable();
				sqlDataAdapter.Fill(result);
				connection.Open();

				int i = query.ExecuteNonQuery();

				if (result.Rows.Count == 1)
				{
					int newCreditValue = 0, newWinsValue = 0;
					foreach (DataRow data in result.Rows)
					{
						newCreditValue = Convert.ToInt32(data["credits"].ToString());
						newWinsValue = Convert.ToInt32(data["wins"].ToString());
					}

					if (newCreditValue < betcredit)
					{
						return "{ \"haserror\": true, \"message\": \"You do not have enough credit to complete this transcation\" }";
					}

					bool won = false;
					string extraCredit = string.Empty;

					if (slot1value == "1.jpg" && slot2value == "1.jpg" && slot3value == "1.jpg")
					{
						newCreditValue += 10;
						newWinsValue += 2;
						extraCredit = "+10";
						won = true;

					}
					else if (slot1value == "2 .jpg" && slot2value == "2.jpg" && slot3value == "2.jpg")
					{
						newCreditValue += 10;
						newWinsValue += 2;
						extraCredit = "+10";
						won = true;

					}
					else if (slot1value == "3.jpg" && slot2value == "3.jpg" && slot3value == "3.jpg")
					{
						newCreditValue += 10;
						newWinsValue += 1;
						won = true;
					}
					else if (slot1value != null && slot2value == "1.jpg" && slot3value == "1.jpg")
					{
						newCreditValue += 2;
						newWinsValue += 1;
						extraCredit = "+2";
						won = true;

					}
					else if (slot1value != null && slot2value == "2.jpg" && slot3value == "2.jpg")
					{
						newCreditValue += 2;
						newWinsValue += 1;
						extraCredit = "+2";
						won = true;
					}
					else if (slot1value != null && slot2value == "3.jpg" && slot3value == "3.jpg")
					{
						newCreditValue += 2;
						newWinsValue += 1;
						extraCredit = "+2";
						won = true;
					}
					else
					{
						newCreditValue -= Convert.ToInt32(betcredit);
					}


					if (won)
					{
						query = new SqlCommand($"update tbl_players set credits = '{newCreditValue}', wins = '{newWinsValue}' where id='{HttpContext.Current.Session["userId"]}'", connection);
						sqlDataAdapter = new SqlDataAdapter(query);

						i = query.ExecuteNonQuery();

						if (i > 0)
						{
							return "{ \"haserror\": false, \"newCreditValue\": \""+ newCreditValue +"\", \"newWinsValue\": \""+ newWinsValue + "\", \"extraCredit\": \"" + extraCredit + "\" }";
						}
					}
					else
					{
						query = new SqlCommand($"update tbl_players set credits = '{newCreditValue}' where id='{HttpContext.Current.Session["userId"]}'", connection);
						sqlDataAdapter = new SqlDataAdapter(query);

						i = query.ExecuteNonQuery();
						if (i > 0)
						{
							return "{ \"haserror\": false, \"newCreditValue\": \""+ newCreditValue + "\", \"newWinsValue\": \""+ newWinsValue + "\", \"extraCredit\": \"" + extraCredit + "\" }";
						}
					}
				}
				else
				{
					return "{ \"haserror\": true, \"message\": \"refresh\" }";
				}
				connection.Close();
			}
			catch (Exception ex)
			{
				return "{ 'haserror': true, 'message': '" + ex.Message  +"' }";
			}

			return "{ \"haserror\": true, \"message\": \"Something went wrong\" }";
		}

	}
}