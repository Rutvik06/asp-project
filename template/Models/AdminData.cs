using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
	public class AdminData
	{
		public string author { get; set; }
		public string email { get; set; }
		public string comment { get; set; }
		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");

		public DataSet selectBook()
		{
			SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_Book]", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}
		public int AddNewCommwnt(string author, string email, string comment)
		{
			SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Add_Comment] (author, email, comment) " +
											"VALUES ('" + author + "', '" + email + "', '" + comment + "')", con);
			con.Open();
			return cmd.ExecuteNonQuery();
		}
	}
}
