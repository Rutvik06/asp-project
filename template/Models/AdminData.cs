using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
	public class AdminData
	{
		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");

		public DataSet selectBook()
		{
			SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_Book]", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}
	}
}
