using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
    public class AddtoCart
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public string BookName { get; set; }
        public string BookQuantity { get; set; }
        public string BookPrice { get; set; }
        public string BookImg { get; set;}
        public DateTime Addedon { get; set; }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
		//public int AddtoCartData(string UserId, string BookName, string BookPrice,string BookQuantity, string BookImg,DateTime AddedOn)
		//{
		//    // Use parameterized query to prevent SQL injection

		//    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Add_To_Cart] (BookName, UserId, BookPrice,BookImg,BookQuantity,AddedOn) " +
		//                                    "VALUES ('" + BookName + "', '" + UserId + "', '" + BookPrice + "', '" + BookImg + "', '" + BookQuantity + "','"+ AddedOn + "')", con);
		//    con.Open();



		//    return cmd.ExecuteNonQuery();

		//}
		public int AddtoCartData(string UserId, string BookName, string BookPrice, string BookQuantity, string BookImg, DateTime AddedOn)
		{
			SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Add_To_Cart] (BookName, UserId, BookPrice, BookImg, BookQuantity, AddedOn) " +
											"VALUES (@BookName, @UserId, @BookPrice, @BookImg, @BookQuantity, @AddedOn)", con);

			cmd.Parameters.AddWithValue("@UserId", UserId);
			cmd.Parameters.AddWithValue("@BookName", BookName);
			cmd.Parameters.AddWithValue("@BookPrice", BookPrice);
			cmd.Parameters.AddWithValue("@BookQuantity", BookQuantity);
			cmd.Parameters.AddWithValue("@BookImg", BookImg);
			cmd.Parameters.AddWithValue("@AddedOn", AddedOn);

			con.Open();
			return cmd.ExecuteNonQuery();
		}

		public DataSet selectAddedBook(int id)
		{
			SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_To_Cart] where id='"+id+"'", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}
		public DataSet selectWithUserId(int UserId)
		{
			SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_To_Cart] where UserId='" + UserId + "'", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}
		public int GetCartItemCount(string userId)
		{
			SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Add_To_Cart] WHERE UserId=@UserId", con);
			cmd.Parameters.AddWithValue("@UserId", userId);
			con.Open();
			int cartItemCount = (int)cmd.ExecuteScalar();
			con.Close();
			return cartItemCount;
		}


	}
}
