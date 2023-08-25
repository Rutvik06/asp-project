using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
    public class UserLogin
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
        public int UserRegister(string username , string email,string password) {
            SqlCommand cmd = new SqlCommand("insert into [dbo].[User_Login](username,email,password)values('"+username+"','"+email+"','"+password+"')",con);
            con.Open();
            return cmd.ExecuteNonQuery();

        }
        public DataSet UserLoginData(string email,string password)
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[User_Login] where email='" + email + "' and password='" + password + "'", con);

			SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
		}
    }
}
