using CommonLayer.Review;
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
    public class ReviewRepository : IReviewRepository
    {
        string connString = "Data Source=LAPTOP-MUFM59UB\\SQLEXPRESS;Initial Catalog=mydatabase;Integrated Security=True;";
        
        public bool AddReview(Review review)
        {
            using(SqlConnection con  = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_AddToReview", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Review", review.BReview);
                cmd.Parameters.AddWithValue("@Stars", review.Stars);
                cmd.Parameters.AddWithValue("@BookId", review.BId);
                cmd.Parameters.AddWithValue("@UserId", review.UId);
                int affected=cmd.ExecuteNonQuery();
                con.Close();
                if (affected > 0)
                {
                    return true;
                }else { return false; }
                
            }
        }

        public List<GetReview> GetReviews()
        {
            using(SqlConnection con =new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetReviewListAll" , con);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader== null)
                {
                    return null;
                }
                List<GetReview> list = new List<GetReview>();
                while (reader.Read()) { 
                    GetReview getReview = new GetReview();
                    getReview.Review = reader[0].ToString();
                    getReview.Stars = Convert.ToInt32(reader[1].ToString());
                    getReview.FullName = reader[2].ToString();
                    getReview.BookId = Convert.ToInt32(reader[3].ToString());
                    list.Add(getReview);
                }
                con.Close();
                return list;

            }
        }

        //C:\Users\rajas\source\repos\BookStore\BookStore\BookStore.csproj


    }
}
