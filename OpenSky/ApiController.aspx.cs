using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace OpenSky
{
    public partial class api : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Lấy địa chỉ IP của người dùng
            string userIP = HttpContext.Current.Request.UserHostAddress;

            // Khởi tạo chuỗi kết nối đến cơ sở dữ liệu SQL Server
            string connectionString = "Data Source=127.0.0.1\\SQLEXPRESS;Initial Catalog=IPSTACK;User Id=sa;Password=123;";

            // Mở kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Khởi tạo SqlCommand để thực thi stored procedure
                using (SqlCommand command = new SqlCommand("AddLocationData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số vào stored procedure
                    command.Parameters.AddWithValue("@ip_address", userIP);
                    command.Parameters.AddWithValue("@country_code", ""); // Điền thông tin về quốc gia nếu có
                    command.Parameters.AddWithValue("@country_name", ""); // Điền thông tin về quốc gia nếu có
                    command.Parameters.AddWithValue("@region_name", ""); // Điền thông tin về khu vực nếu có
                    command.Parameters.AddWithValue("@city", ""); // Điền thông tin về thành phố nếu có
                    command.Parameters.AddWithValue("@zip", DBNull.Value); // Điền mã zip nếu có
                    command.Parameters.AddWithValue("@latitude", DBNull.Value); // Điền vĩ độ nếu có
                    command.Parameters.AddWithValue("@longitude", DBNull.Value); // Điền kinh độ nếu có

                    try
                    {
                        // Mở kết nối
                        connection.Open();

                        // Thực thi stored procedure
                        command.ExecuteNonQuery();

                        // Trả về phản hồi thành công
                        Response.ContentType = "application/json";
                        Response.Write("{\"success\": true}");
                    }
                    catch (Exception ex)
                    {
                        // Trả về thông báo lỗi nếu có lỗi xảy ra
                        Response.ContentType = "application/json";
                        Response.Write("{\"error\": \"" + ex.Message + "\"}");
                    }
                }
            }
        }
    }
}
