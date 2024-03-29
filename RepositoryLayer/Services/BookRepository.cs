using CommonLayer.BookModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class BookRepository : IBookRepository
    {
        string connString = "Data Source=LAPTOP-MUFM59UB\\SQLEXPRESS;Initial Catalog=mydatabase;Integrated Security=True;";

         public Book GetBookById(int Id)
        {
            using(SqlConnection con  = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from BookStore where Id="+Id, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr == null)
                {
                    return null;
                }
                Book book = new Book();
                while (dr.Read())
                {
                    book.Id = Convert.ToInt32(dr[0].ToString());
                    book.Title = dr[1].ToString();
                    book.Price = Convert.ToDouble(dr[2].ToString());
                    book.Author = dr[3].ToString();
                    book.Description = dr[4].ToString();
                    book.Quantity= Convert.ToInt32(dr[5].ToString());
                    book.Image = dr[6].ToString();
                    book.ActualPrice =Convert.ToDouble(dr[7].ToString());
                }
                return book;

            }
        }



        public Book GetBookByTitle(string Title)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from BookStore where Title='" + Title+"'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr == null)
                {
                    return null;
                }
                Book book = new Book();
                while (dr.Read())
                {
                    book.Id = Convert.ToInt32(dr[0].ToString());
                    book.Title = dr[1].ToString();
                    book.Price = Convert.ToDouble(dr[2].ToString());
                    book.Author = dr[3].ToString();
                    book.Description = dr[4].ToString();
                    book.Quantity = Convert.ToInt32(dr[5].ToString());
                    book.Image = dr[6].ToString();
                    book.ActualPrice = Convert.ToDouble(dr[7].ToString());
                }
                return book;

            }
        }

        public List<Book> GetAllBooks()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from BookStore", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr == null)
                {
                    return null;
                }

                List<Book> list = new List<Book>();
                while (dr.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dr[0].ToString());
                    book.Title = dr[1].ToString();
                    book.Price = Convert.ToDouble(dr[2].ToString());
                    book.Author = dr[3].ToString();
                    book.Description = dr[4].ToString();
                    book.Quantity = Convert.ToInt32(dr[5].ToString());
                    book.Image = dr[6].ToString();
                    book.ActualPrice = Convert.ToDouble(dr[7].ToString());
                    list.Add(book);
                }
                return list;

            }
        }

        public List<Book> GetBooksByAutherName(string Auther)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from BookStore where Auther='"+Auther+"'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr == null)
                {
                    return null;
                }

                List<Book> list = new List<Book>();
                while (dr.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(dr[0].ToString());
                    book.Title = dr[1].ToString();
                    book.Price = Convert.ToDouble(dr[2].ToString());
                    book.Author = dr[3].ToString();
                    book.Description = dr[4].ToString();
                    book.Quantity = Convert.ToInt32(dr[5].ToString());
                    book.Image = dr[6].ToString();
                    book.ActualPrice = Convert.ToDouble(dr[7].ToString());
                    list.Add(book);
                }
                return list;

            }
        }

        public Book GetBookByTitleandAuther(string Title, string Auther)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from BookStore where Title='" + Title + "' and Auther='"+Auther+"'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr == null)
                {
                    return null;
                }
                Book book = new Book();
                while (dr.Read())
                {
                    book.Id = Convert.ToInt32(dr[0].ToString());
                    book.Title = dr[1].ToString();
                    book.Price = Convert.ToDouble(dr[2].ToString());
                    book.Author = dr[3].ToString();
                    book.Description = dr[4].ToString();
                    book.Quantity = Convert.ToInt32(dr[5].ToString());
                    book.Image = dr[6].ToString();
                    book.ActualPrice =Convert.ToDouble(dr[7].ToString());
                }
                return book;

            }
        }


        public bool UpdateBook(Book book)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateBook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", book.Id);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Price", book.Price);
                cmd.Parameters.AddWithValue("@Auther", book.Author);
                cmd.Parameters.AddWithValue("@Description", book.Description);
                cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                cmd.Parameters.AddWithValue("@Image", book.Image);
                cmd.Parameters.AddWithValue("@Act_Price", book.ActualPrice);

                int affected=cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public bool CreateBook(InsertBook book)
        {
            using(SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_AddBook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Price", book.Price);
                cmd.Parameters.AddWithValue("@Auther",book.Author );
                cmd.Parameters.AddWithValue("@Description",book.Description );
                cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                cmd.Parameters.AddWithValue("@Image", book.Image);
                cmd.Parameters.AddWithValue("@Act_Price", book.ActualPrice);

                int affected=cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                con.Close();
            }
            return false;
        }





        //=====================================================================================================================================

        public bool AddToCart(int UId, BookCart cart)
        {
            using( SqlConnection con = new SqlConnection(connString)) { 
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_AddCart", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", cart.BookId);
                cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@UserId", UId);
                int affected= cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    return true;
                }
                con.Close();
            }
            return false;
        }


        public List<UserCartDetails> UserCartDetails(int UId)
        {
            List<UserCartDetails> list = new List<UserCartDetails>();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"sp_GetCartBooksDetails {UId}", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader==null)
                {
                    return null;
                }
                
                while (reader.Read())
                {
                    UserCartDetails userCartDetails = new UserCartDetails();
                    userCartDetails.Id = Convert.ToInt32(reader[0]);
                    userCartDetails.Title = reader[1].ToString();
                    userCartDetails.Auther = reader[2].ToString();
                    userCartDetails.Price =Convert.ToDouble( reader[3].ToString());
                    userCartDetails.Description = reader[4].ToString();
                    userCartDetails.Image = reader[5].ToString();
                    userCartDetails.ActualPrice =Convert.ToDouble( reader[6].ToString());
                    userCartDetails.Quantity =Convert.ToInt32( reader[7].ToString());
                    list.Add(userCartDetails);  

                }
                con.Close();
                
            }
            return list;
        }


        public BookCart UpdateBookQuantity(int UId, BookCart cart)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"update UserBookCart set Quantity={cart.Quantity} where BookId={cart.BookId} and UserId={UId}", con);
                int affected = cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    return cart;
                }
                con.Close();
            }
            return null;
        }



        public double GetPriceInCart(int userId)
        {
            List<UserCartDetails> bookList = UserCartDetails(userId);
            double totalPrice = 0;
            foreach (var book in bookList)
            {
                totalPrice += (book.Quantity * book.Price);
            }
            return totalPrice;
        }


        public bool RemoveCartItem(RemoveCartItem removeCartItem)
        {
            using(SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"delete from UserBookCart where UserId={removeCartItem.UId} and BookId={removeCartItem.BookId}", con);
                int affected = cmd.ExecuteNonQuery();
                con.Close() ;
                if (affected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
