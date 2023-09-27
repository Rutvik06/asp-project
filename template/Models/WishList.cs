using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
	public class WishList
	{
		public int Id { get; set; }	
		public string BookName { get; set; }
		public string BookPrice { get; set; }
		public string BookImg { get; set; }
		public string userId { get; set; }
		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
		public int AddToWishList(string userId, string BookName, string BookPrice, string BookImg)
		{
			SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[WishList] (userId, BookName, BookPrice, BookImg) " +
											"VALUES ('" + userId + "', '" + BookName + "', '" + BookPrice + "', '" + BookImg + "')", con);
			con.Open();
			return cmd.ExecuteNonQuery();
		}

		public DataSet SelectData(int userId)
		{
			SqlCommand cmd = new SqlCommand("SELECT c.* FROM [dbo].[WishList] c INNER JOIN [dbo].[User_Login] l ON c.userId = l.id WHERE l.id = '" + userId + "'", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}
		public int deleteBook(int id)
		{
			SqlCommand cmd = new SqlCommand("delete from [dbo].[WishList] where id='" + id + "'", con);
			con.Open();

			return cmd.ExecuteNonQuery();
		}
	}
}
