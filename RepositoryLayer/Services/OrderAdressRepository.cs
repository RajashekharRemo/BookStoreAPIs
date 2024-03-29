using CommonLayer.OrderAddress;
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
    public class OrderAdressRepository : IOrderAdressRepository
    {
        string connString = "Data Source=LAPTOP-MUFM59UB\\SQLEXPRESS;Initial Catalog=mydatabase;Integrated Security=True;";

        public List<Orders> GetAllOrderes(int UId)
        {
            using(SqlConnection con = new SqlConnection(connString)) { 
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetOrderDetails", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UId", UId);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader == null)
                {
                    return null;
                }
                List<Orders> orders = new List<Orders>();
                while (reader.Read())
                {
                    Orders o = new Orders();
                    o.Id = Convert.ToInt32(reader[0].ToString());
                    o.UName = reader[1].ToString();
                    o.Phone = Convert.ToInt64(reader[2].ToString());
                    o.Address = reader[3].ToString();
                    o.City= reader[4].ToString();
                    o.State= reader[5].ToString();
                    o.Quantity = Convert.ToInt32(reader[6].ToString());
                    o.Title = reader[7].ToString();
                    o.Description = reader[8].ToString();
                    o.Auther = reader[9].ToString();
                    o.Image = reader[10].ToString();
                    o.Price =(float) Convert.ToDouble(reader[11].ToString());
                    o.ActualPrice =(float) Convert.ToDouble(reader[12].ToString());
                    o.OrderDate= reader[13].ToString();
                    orders.Add(o);

                }
                con.Close();
                return orders;
            }


        }

        public bool AddOrder(int Uid, OrderToDB toDB)
        {
            using(SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_AddBookOrders", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Uname", toDB.UName);
                cmd.Parameters.AddWithValue("@UPhone", toDB.Phone);
                cmd.Parameters.AddWithValue("@City", toDB.City);
                cmd.Parameters.AddWithValue("@State", toDB.State);
                cmd.Parameters.AddWithValue("@Address", toDB.Address);
                cmd.Parameters.AddWithValue("@BookId", toDB.BookId);
                cmd.Parameters.AddWithValue("@Quantity", toDB.Quantity);
                cmd.Parameters.AddWithValue("@UserId", Uid);
                cmd.Parameters.AddWithValue("@DateofOder", DateTime.Now);
                int affected=cmd.ExecuteNonQuery();
                con.Close();
                if(affected > 0)
                {
                    return true;
                }else { return false; }
            }
        }




        public bool AddAddress(int UId, UpdateAddress address)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_AddUserAddressForBookStore", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UName", address.UName);
                cmd.Parameters.AddWithValue("@UPhone", address.UPhone);
                cmd.Parameters.AddWithValue("@City",address.City );
                cmd.Parameters.AddWithValue("@State",address.State );
                cmd.Parameters.AddWithValue("@Address", address.UAddress);
                cmd.Parameters.AddWithValue("@AddressType",address.AddressType );
                cmd.Parameters.AddWithValue("@UserId", UId);
                int affected=cmd.ExecuteNonQuery();
                conn.Close();
                if(affected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public bool UpdateAddress(int UId, Address address)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateUserAddress", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", address.UName);
                cmd.Parameters.AddWithValue("@Phone", address.UPhone);
                cmd.Parameters.AddWithValue("@City", address.City);
                cmd.Parameters.AddWithValue("@State", address.State);
                cmd.Parameters.AddWithValue("@Address", address.UAddress);
                cmd.Parameters.AddWithValue("@AddType", address.AddressType);
                cmd.Parameters.AddWithValue("@UId", UId);
                int affected = cmd.ExecuteNonQuery();
                conn.Close();
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

        public List<Address> GetAddresses(int UId)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from UserAddressForBookStore where UserId="+UId, conn) ;
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader== null)
                {
                    return null;
                }
                List<Address> addressesList = new List<Address>();  
                while(reader.Read())
                {
                    Address address = new Address();
                    address.Id = Convert.ToInt32(reader[0].ToString());
                    address.UName = reader[1].ToString();
                    address.UPhone= Convert.ToInt64( reader[2].ToString());
                    address.City = reader[3].ToString();
                    address.State = reader[4].ToString();
                    address.UAddress = reader[5].ToString();
                    address.AddressType = reader[6].ToString();
                    addressesList.Add(address);

                }
                conn.Close ();
                return addressesList;

            }

        }




    }
}
