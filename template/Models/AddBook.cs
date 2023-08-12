using System.Data.SqlClient;

namespace template.Models
{
    //public class AddBook
    //{
    //	public string BookName { get; set; }
    //	public string BookCategory { get; set; }
    //	public string BookAuthor { get; set; }
    //	public string BookImage { get; set; }
    //	public string BookPrice { get; set; }	
    //	public string BookDescription { get; set; }
    //	SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
    //	public int AddNewBook(string BookName, string BookImage,string BookCategory, string BookAuthor, string BookPrice , string BookDescription)
    //	{
    //		SqlCommand cmd = new SqlCommand("insert into [dbo].[AddBook](BookName,BookCategory,BookPrice,BookDescription,BookAuthor,BookImage)values" +
    //			"('" + BookName + "','" + BookCategory + "','" + BookPrice+ "','"+BookDescription+"','"+BookAuthor+"','"+BookImage+"')", con);
    //		con.Open();

    //		return cmd.ExecuteNonQuery();
    //	}
    public class AddBook
    {
        public string BookName { get; set; }
        public string BookCategory { get; set; }
        public string BookAuthor { get; set; }
        public string BookImage { get; set; }
        public string BookPrice { get; set; }
        public string BookDescription { get; set; }

        public int AddNewBook(string BookName, string BookCategory, string BookPrice, string BookDescription, string BookAuthor, string BookImage)
        {
        using (SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345"))
            // Use parameterized query to prevent SQL injection
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[AddBook] (BookName, BookCategory, BookPrice, BookDescription, BookAuthor, BookImage) " +
                                                "VALUES (@BookName, @BookCategory, @BookPrice, @BookDescription, @BookAuthor, @BookImage)", con);

                cmd.Parameters.AddWithValue("@BookName", BookName);
                cmd.Parameters.AddWithValue("@BookCategory", BookCategory);
                cmd.Parameters.AddWithValue("@BookPrice", BookPrice);
                cmd.Parameters.AddWithValue("@BookDescription", BookDescription);
                cmd.Parameters.AddWithValue("@BookAuthor", BookAuthor);
                cmd.Parameters.AddWithValue("@BookImage", BookImage);

                return cmd.ExecuteNonQuery();
            }
        }

    }

}
