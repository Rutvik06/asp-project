using System.Data.SqlClient;
using System.Data;

namespace template.Models
{
    public class ViewUserBooks
    {
		public string id { get; set; }

		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");

        public DataSet selectUserSideBooks()
        {
            SqlCommand cmd = new SqlCommand("select * from[dbo].[AddBook]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
		public DataSet selectUserSideBookSingleBook(string id)
		{
			SqlCommand cmd = new SqlCommand("select * from[dbo].[AddBook] where id = '"+id+"'", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}
	}
}
