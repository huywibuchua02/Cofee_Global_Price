using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlightApp
{
    public partial class api : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.QueryString["action"];
            if (action == "get")
            {
                GetFlightData();
            }
            else if (action == "insert")
            {
                InsertFlightData();
            }
            else
            {
                this.Response.ContentType = "application/json";
                this.Response.Write("{\"ok\":0,\"msg\":\"Invalid action\"}");
            }
        }

        private void GetFlightData()
        {
            string connectionString = "Data Source=127.0.0.1\\SQLEXPRESS;Initial Catalog=flight_data;User Id=sa;Password=123;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM flights", connection))
                {
                    command.CommandType = CommandType.Text;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                        this.Response.ContentType = "application/json";
                        this.Response.Write(json);
                    }
                    catch (Exception ex)
                    {
                        this.Response.ContentType = "application/json";
                        this.Response.Write("{\"ok\":0,\"msg\":\"" + ex.Message + "\"}");
                    }
                }
            }
        }

        private void InsertFlightData()
        {
            string connectionString = "Data Source=127.0.0.1\\SQLEXPRESS;Initial Catalog=flight_data;User Id=sa;Password=123;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_InsertFlightData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@icao24", Request.Form["icao24"]);
                    command.Parameters.AddWithValue("@callsign", Request.Form["callsign"]);
                    command.Parameters.AddWithValue("@origin_country", Request.Form["origin_country"]);
                    command.Parameters.AddWithValue("@time_position", Request.Form["time_position"]);
                    command.Parameters.AddWithValue("@last_contact", Request.Form["last_contact"]);
                    command.Parameters.AddWithValue("@longitude", Request.Form["longitude"]);
                    command.Parameters.AddWithValue("@latitude", Request.Form["latitude"]);
                    command.Parameters.AddWithValue("@baro_altitude", Request.Form["baro_altitude"]);
                    command.Parameters.AddWithValue("@on_ground", Request.Form["on_ground"]);
                    command.Parameters.AddWithValue("@velocity", Request.Form["velocity"]);
                    command.Parameters.AddWithValue("@true_track", Request.Form["true_track"]);
                    command.Parameters.AddWithValue("@vertical_rate", Request.Form["vertical_rate"]);
                    command.Parameters.AddWithValue("@sensors", Request.Form["sensors"]);
                    command.Parameters.AddWithValue("@geo_altitude", Request.Form["geo_altitude"]);
                    command.Parameters.AddWithValue("@squawk", Request.Form["squawk"]);
                    command.Parameters.AddWithValue("@spi", Request.Form["spi"]);
                    command.Parameters.AddWithValue("@position_source", Request.Form["position_source"]);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        this.Response.ContentType = "application/json";
                        this.Response.Write("{\"ok\":1,\"msg\":\"Success\"}");
                    }
                    catch (Exception ex)
                    {
                        this.Response.ContentType = "application/json";
                        this.Response.Write("{\"ok\":0,\"msg\":\"" + ex.Message + "\"}");
                    }
                }
            }
        }
    }
}
