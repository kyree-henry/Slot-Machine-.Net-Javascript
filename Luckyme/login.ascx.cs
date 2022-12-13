using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace Luckyme
{
    public partial class login : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{ }

        protected void GotoRegister(object sender, EventArgs e)
        {
            Response.Redirect("register.aspx");
        }

        protected void SubmitForm(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand query = new SqlCommand($"select * from tbl_players where username='{username.Text}' and password='{password.Text}'", connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable result =  new DataTable();
                sqlDataAdapter.Fill(result);
                connection.Open();

                int i = query.ExecuteNonQuery();
                connection.Close();

                if(result.Rows.Count == 1) 
                {
                    foreach(DataRow data in result.Rows)
                    {
                        Session["userId"] = data["id"].ToString();
                        Session["userName"] = data["username"].ToString();
                    }

                    Response.Redirect("default.aspx");
                }
                else
                {
                    error.IsValid = false;
                    error.ErrorMessage = "Invalid Login Attempt!";
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