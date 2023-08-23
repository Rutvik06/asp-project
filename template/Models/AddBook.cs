using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
    
    public class AddBook
    {
        public string BookName { get; set; }
        public string BookCategory { get; set; }
        public string BookAuthor { get; set; }
        public string BookImage { get; set; }
        public string BookPrice { get; set; }
        public string BookDescription { get; set; }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
        public int AddNewBook(string BookName, string BookCategory, string BookPrice, string BookDescription, string BookAuthor, string BookImage)
        {
            // Use parameterized query to prevent SQL injection
            
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[AddBook] (BookName, BookCategory, BookPrice, BookDescription, BookAuthor, BookImage) " +
                                                "VALUES ('"+BookName+"', '"+BookCategory+"', '"+BookPrice+"', '"+BookDescription+"', '"+BookAuthor+"', '"+BookImage+"')", con);
                con.Open();

               

                return cmd.ExecuteNonQuery();
            
        }

        //public DataSet selectNewBook()
        //{
        //    SqlCommand cmd = new SqlCommand("select * from[dbo].[AddBook]", con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);

        //    return ds;
        //}

    }

}
