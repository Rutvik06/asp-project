using System.Data.SqlClient;
using System.Data;

namespace template.Models
{
    public class UserList
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");

        public DataSet selectUserList()
        {
            SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_Account]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
    }
}
