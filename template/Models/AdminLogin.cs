using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
    public class AdminLogin
    {
        public string email { get; set; }  
        public string password { get; set; }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
        public DataSet AdminLoginData(string email, string password)
        {
            SqlCommand cmd = new SqlCommand("select * from [dbo].[Admin_Login] where email='"+email+"'AND password='"+password+"'",con);
            SqlDataAdapter da= new SqlDataAdapter(cmd);
            DataSet ds= new DataSet();
            da.Fill(ds);
            return ds;

        }
    }
}
