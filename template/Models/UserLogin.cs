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
            SqlCommand cmd = new SqlCommand("insert into [dbo].[UserLogin](username,email,password)values('"+username+"','"+email+"','"+password+"')",con);
            con.Open();
            return cmd.ExecuteNonQuery();

        }
        public DataSet UserLoginData(string email,string password)
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[UserLogin] where email='" + email + "'AND password='" + password + "'", con);

			SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
		}
    }
}
