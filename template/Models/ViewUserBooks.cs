using System.Data.SqlClient;
using System.Data;

namespace template.Models
{
    public class ViewUserBooks
    {
		public int id { get; set; }

		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");

        public DataSet selectUserSideBooks()
        {
            SqlCommand cmd = new SqlCommand("select * from[dbo].[AddBook]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
		//public DataSet selectUserSideBookSingleBook(int id)
		//{
		//	SqlCommand cmd = new SqlCommand("select * from[dbo].[AddBook] where id = '"+id+"'", con);
		//	SqlDataAdapter da = new SqlDataAdapter(cmd);
		//	DataSet ds = new DataSet();
		//	da.Fill(ds);

		//	return ds;
		//}
		public DataSet selectUserSideBookSingleBook(int id)
		{
			SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[AddBook] WHERE id = @id", con);
			cmd.Parameters.AddWithValue("@id", id); // Use parameters to prevent SQL injection
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}

	}
}
