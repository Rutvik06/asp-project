using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
    public class AddAuthor
    {
        public int id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorDescription { get; set; } 
        public string AuthorEmail { get; set; }
        public string AuthorImg { get; set; }
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
        public int AddNewBook(string AuthorName, string AuthorDescription, string AuthorEmail, string AuthorImg)
        {
            // Use parameterized query to prevent SQL injection

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Add_Author] (AuthorName, AuthorDescription, AuthorEmail, AuthorImg) " +
                                            "VALUES ('" + AuthorName + "', '" + AuthorDescription + "', '" + AuthorEmail + "', '" + AuthorImg + "')", con);
            con.Open();



            return cmd.ExecuteNonQuery();

        }
        public DataSet selectNewAuthor()
        {
            SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_Author]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
        public int deleteAuthor(int id)
        {
            SqlCommand cmd = new SqlCommand("delete from [dbo].[Add_Author] where id='" + id + "'", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }
        public int updateAuthor(int id,string AuthorName, string AuthorDescription, string AuthorEmail, string AuthorImg)
        {
            SqlCommand cmd = new SqlCommand("update [dbo].[Add_Author] set AuthorName='" + AuthorName + "',AuthorDescription='" + AuthorDescription + "' ,AuthorEmail='" + AuthorEmail + "' ,AuthorImg='" + AuthorImg + "' where id='" + id + "'", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }
        public DataSet selectSinAuthor(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Add_Author] WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

    }
}
