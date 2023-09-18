using System.Data.SqlClient;

namespace template.Models
{
	public class WishList
	{
		public int Id { get; set; }	
		public string BookName { get; set; }
		public string BookPrice { get; set; }
		public string BookImg { get; set; }
		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
		public int AddToWishList(string BookName, string BookPrice, string BookImg)
		{
			SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[WishList] (BookName, BookPrice, BookImg) " +
											"VALUES ('" + BookName + "',  '" + BookPrice + "', '" + BookImg + "')", con);
			con.Open();
			return cmd.ExecuteNonQuery();
		}
	}
}
