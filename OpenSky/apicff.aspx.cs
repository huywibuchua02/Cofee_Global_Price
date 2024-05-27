using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cofee
{
    public partial class apicff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string action = Request["action"];
            switch (action)
            {
                case "get_all_coffee":
                    get_all_coffee(action);
                    break;
            }

        }

        void get_all_coffee(string action)
        {

            string connectionString = "Data Source=127.0.0.1\\SQLEXPRESS;Initial Catalog=IPSTACK;User Id=sa;Password=123;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("GetCoffeePrices", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số action vào stored procedure
                    command.Parameters.AddWithValue("@action", action);

                    // Thực thi stored procedure
                    string jsonStr = (string)command.ExecuteScalar();
                    Response.Write(jsonStr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

    }
}