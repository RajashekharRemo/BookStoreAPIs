using CommonLayer.BookModel;
using CommonLayer.WishList;
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
    public class WishList : IWishList
    {
        string connString = "Data Source=LAPTOP-MUFM59UB\\SQLEXPRESS;Initial Catalog=mydatabase;Integrated Security=True;";
        public bool AddToWishList(WishListClass wishList)
        {
            using(SqlConnection con  = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_AddToWishList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", wishList.UId);
                cmd.Parameters.AddWithValue("@BookId", wishList.BId);
                int affected = cmd.ExecuteNonQuery();
                if(affected > 0)
                {
                    return true;
                }else { return false; }
            }
        }

        public List<Book> GetWishList(int UId)
        {
            using(SqlConnection con= new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetWishList "+UId, con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader == null)
                {
                    return null;
                }
                List<Book> list = new List<Book>();
                while(reader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32( reader[0].ToString());
                    book.Title = reader[1].ToString();
                    book.Description = reader[2].ToString();
                    book.Author = reader[3].ToString();
                    book.Image = reader[4].ToString();
                    book.Price = Convert.ToDouble(reader[5].ToString());
                    book.ActualPrice= Convert.ToDouble(reader[6].ToString());
                    book.Quantity = Convert.ToInt32(reader[7].ToString());
                    list.Add(book);

                }

                con.Close();
                return list;

            }
        }

        public bool RemoveWishListItem(RemoveCartItem removeCartItem)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"delete from NoteWishList where UserId={removeCartItem.UId} and BookId={removeCartItem.BookId}", con);
                int affected = cmd.ExecuteNonQuery();
                con.Close();
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
