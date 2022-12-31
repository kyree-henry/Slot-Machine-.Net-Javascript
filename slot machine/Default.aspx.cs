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

		[WebMethod(EnableSession = true)]
		public static string LoadPage(string pageType)
		{
			using (Page page = new Page())
			{
				UserControl userControl = (UserControl)page.LoadControl($"~/{pageType}.ascx");
				page.Controls.Add(userControl);
				using (StringWriter writer = new StringWriter())
				{
					page.Controls.Add(userControl);
					HttpContext.Current.Server.Execute(page, writer, false);
					return writer.ToString();
				}
			}
		}

		[WebMethod(EnableSession = true)]
		public static string Login(string userName, string password)
		{
			string connectionString = WebConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				SqlCommand query = new SqlCommand($"select * from tbl_players where username='{userName}' and password='{password}'", connection);
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
						HttpContext.Current.Session["id"] = data["id"].ToString();
					}

					return "{ \"succeeded\": true, \"message\": \"Login Succeeded! \" }";
				}
				else
				{
					return "{ \"succeeded\": false, \"message\": \"Invalid Login Attempt! \" }";
				}

			}
			catch (Exception ex)
			{
				return "{ \"succeeded\": false, \"message\": \"" + ex.Message + "\" }";
			}

		}
		
		[WebMethod(EnableSession = true)]
		public static string Register(string userName, string password)
		{
			string connectionString = WebConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				SqlCommand query = new SqlCommand($"select * from tbl_players where username='{userName}'", connection);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
				DataTable result = new DataTable();
				sqlDataAdapter.Fill(result);
				connection.Open();

				int i = query.ExecuteNonQuery();

				if (result.Rows.Count > 0)
				{
					return "{ \"succeeded\": false, \"message\": \" User with username '" + userName + "' already exist! \" }";
				}
				else
				{
					query = new SqlCommand($"insert into tbl_players (username, password, credits, wins) values ('{userName}','{password}', 5, 0)", connection);
					sqlDataAdapter = new SqlDataAdapter(query);

					i = query.ExecuteNonQuery();

					if (i > 0) return "{ \"succeeded\": true, \"message\": \"Registration Successfull! \" }";					
				}
			}
			catch (Exception ex)
			{
				return "{ \"succeeded\": false, \"message\": \"" + ex.Message + "\" }";
			}

			return "{ \"succeeded\": false, \"message\": \"an error occured somewhere!\" }";
		}

		[WebMethod(EnableSession = true)]
		public static string Payout(string slot1value, string slot2value, string slot3value, int betcredit)
		{
			string connectionString = WebConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				SqlCommand query = new SqlCommand($"select * from tbl_players where id='{HttpContext.Current.Session["id"]}'", connection);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
				DataTable result = new DataTable();
				sqlDataAdapter.Fill(result);
				connection.Open();

				int i = query.ExecuteNonQuery();

				if (result.Rows.Count == 1)
				{
					int credits = 0, wins = 0;
					foreach (DataRow data in result.Rows)
					{
						credits = Convert.ToInt32(data["credits"].ToString());
						wins = Convert.ToInt32(data["wins"].ToString());
					}

					if (credits < betcredit)
					{
						return "{ \"haserror\": true, \"message\": \"You do not have enough credit to complete this event! \" }";
					}

					bool won = false;
					string extraCredit = string.Empty;

					if (slot1value == "1.jpg" && slot2value == "1.jpg" && slot3value == "1.jpg")
					{
						credits += 10;
						wins += 2;
						extraCredit = "+10";
						won = true;

					}
					else if (slot1value == "2 .jpg" && slot2value == "2.jpg" && slot3value == "2.jpg")
					{
						credits += 10;
						wins += 2;
						extraCredit = "+10";
						won = true;

					}
					else if (slot1value == "3.jpg" && slot2value == "3.jpg" && slot3value == "3.jpg")
					{
						credits += 10;
						wins += 1;
						won = true;
					}
					else if (slot1value != null && slot2value == "1.jpg" && slot3value == "1.jpg")
					{
						credits += 2;
						wins += 1;
						extraCredit = "+2";
						won = true;

					}
					else if (slot1value != null && slot2value == "2.jpg" && slot3value == "2.jpg")
					{
						credits += 2;
						wins += 1;
						extraCredit = "+2";
						won = true;
					}
					else if (slot1value != null && slot2value == "3.jpg" && slot3value == "3.jpg")
					{
						credits += 2;
						wins += 1;
						extraCredit = "+2";
						won = true;
					}
					else
					{
						credits -= Convert.ToInt32(betcredit);
					}


					if (won)
					{
						query = new SqlCommand($"update tbl_players set credits = '{credits}', wins = '{wins}' where id='{HttpContext.Current.Session["id"]}'", connection);
						sqlDataAdapter = new SqlDataAdapter(query);

						i = query.ExecuteNonQuery();

						if (i > 0)
						{
							return "{ \"succeeded\": true, \"credits\": \"" + credits +"\", \"wins\": \""+ wins + "\", \"extraCredit\": \"" + extraCredit + "\" }";
						}
					}
					else
					{
						query = new SqlCommand($"update tbl_players set credits = '{credits}' where id='{HttpContext.Current.Session["id"]}'", connection);
						sqlDataAdapter = new SqlDataAdapter(query);

						i = query.ExecuteNonQuery();
						if (i > 0)
						{
							return "{ \"succeeded\": true, \"credits\": \"" + credits + "\", \"wins\": \""+ wins + "\", \"extraCredit\": \"" + extraCredit + "\" }";
						}
					}
				}
				connection.Close();
			}
			catch (Exception ex)
			{
				return "{ 'succeeded': false, 'message': '" + ex.Message  +"' }";
			}

			return "{ \"succeeded\": false, \"message\": \"an error occured somewhere! \" }";
		}

		[WebMethod(EnableSession = true)]
		public static string AddCredit()
		{
			string connectionString = WebConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				SqlCommand query = new SqlCommand($"select * from tbl_players where id='{HttpContext.Current.Session["id"]}'", connection);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
				DataTable result = new DataTable();
				sqlDataAdapter.Fill(result);
				connection.Open();

				int i = query.ExecuteNonQuery();

				if (result.Rows.Count == 1)
				{
					int credits = 0;
					foreach (DataRow data in result.Rows)
					{
						credits = Convert.ToInt32(data["credits"].ToString());
					}

					credits += 5;

					query = new SqlCommand($"update tbl_players set credits = '{credits}' where id='{HttpContext.Current.Session["id"]}'", connection);
					sqlDataAdapter = new SqlDataAdapter(query);

					i = query.ExecuteNonQuery();

					if (i > 0)
					{
						return "{ \"succeeded\": true, \"credits\": \"" + credits + "\" }";
					}

				}
				connection.Close();
			}
			catch (Exception ex)
			{
				return "{ \"succeeded\": false, \"message\": \"" + ex.Message + "\" }";
			}

			return "{ \"succeeded\": false, \"message\": \"an error occured somewhere! \" }";
		}

		[WebMethod(EnableSession = true)]
		public static string Exit()
		{
			HttpContext.Current.Session.Clear();
			return "";
		}

	}
}