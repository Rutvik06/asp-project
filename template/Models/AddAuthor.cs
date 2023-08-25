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

            return cmd.ExecuteNonQuery();
        }
    }
}
