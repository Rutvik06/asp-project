using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
    public class Cart_Count
    {
        public int id { get; set; }
        public int UserId { get; set; }

        public int CartCount { get; set; }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
        public DataSet GetCartCount(int UserId)
        {
            
            
                SqlCommand cmd = new SqlCommand("select * from[dbo].[Cart_Count] where UserId='"+UserId+"'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            

        }
    }
}
