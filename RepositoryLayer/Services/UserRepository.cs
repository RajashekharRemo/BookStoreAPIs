using CommonLayer.UserModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        string connString = "Data Source=LAPTOP-MUFM59UB\\SQLEXPRESS;Initial Catalog=mydatabase;Integrated Security=True;";
        public static string Key = "gdvade!@edef";

        private readonly IConfiguration _config;
        public UserRepository (IConfiguration config)
        {
            _config = config;
        }

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) { return ""; }
            var decryptBytes = Convert.FromBase64String(password);
            var result = Encoding.UTF8.GetString(decryptBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }


        public string GenerateToken(int id, string Email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim("Email", Email),
            new Claim("UserId", id.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


















        public List<GetUser> getUsers()
        {
            List<GetUser> list = new List<GetUser>();

            using(SqlConnection con = new SqlConnection(connString)) { 
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from BookStoreUser", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GetUser user = new GetUser();
                    user.Id = Convert.ToInt32(reader[0].ToString());
                    user.FullName = reader[1].ToString();
                    user.Email = reader[2].ToString();
                    user.Phone = Convert.ToInt64( reader[4].ToString());
                    user.Role= reader[5].ToString();
                    list.Add(user);
                }
                con.Close();
            }
            return list;    
        }

        public GetUser getUserById(int UId)
        {

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from BookStoreUser where Id="+UId, con);
                SqlDataReader reader = cmd.ExecuteReader();
                GetUser user = new GetUser();
                while (reader.Read())
                {
                    user.Id = Convert.ToInt32(reader[0].ToString());
                    user.FullName = reader[1].ToString();
                    user.Email = reader[2].ToString();
                    user.Phone = Convert.ToInt64(reader[4].ToString());
                    user.Role = reader[5].ToString();
                }
                con.Close();

                if (user == null)
                {
                    return null;
                }
                else
                {
                    return user;
                }
            }
        }



        public GetUser Create(User user)
        {
            using(SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("cp_Add_BookStoreUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", user.FullName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                string password=EncryptPassword(user.Password);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Phone", user.Phosne);
                int affected=cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    GetUser getUser = new GetUser();
                    SqlCommand cmd2 = new SqlCommand("cp_Get_BookStoreUserByEmail", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@Email", user.Email);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {

                        getUser.Id = Convert.ToInt32(reader[0].ToString());
                        getUser.FullName = reader[1].ToString();
                        getUser.Email = reader[2].ToString();
                        getUser.Phone = Convert.ToInt64(reader[4].ToString());
                    }
                    con.Close();
                    return getUser;
                }
                else
                {
                    return null;
                }

            }
        }


        
        public TokenEmailClass Login(LoginUser loginUser)
        {
            using(SqlConnection con=new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("cp_Get_BookStoreUserByEmail", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@Email", loginUser.Email);
                SqlDataReader reader = cmd2.ExecuteReader();
                
                while (reader.Read())
                {
                    string password =DecryptPassword(reader[3].ToString());
                    if (loginUser.Email == reader[2].ToString() && loginUser.Password == password) {
                        TokenEmailClass tokenEmailClass = new TokenEmailClass();
                        tokenEmailClass.Id = Convert.ToInt32( reader[0].ToString());
                        string[] res = reader[1].ToString().Split();
                        tokenEmailClass.FullName = res[0];
                        tokenEmailClass.Email = reader[2].ToString();
                        tokenEmailClass.Token = GenerateToken(Convert.ToInt32(reader[0].ToString()), reader[2].ToString());
                        return tokenEmailClass;
                    }
                    else
                    {
                        return null;
                    }
                }
                con.Close();

            }
            return null;
        }
        


        public string Update(int id, UpdateUser updateUser)
        {
            using(SqlConnection con=new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("cp_Update_BookStoreUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", updateUser.FullName);
                cmd.Parameters.AddWithValue("@Email", updateUser.Email);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Phone", updateUser.Phone);
                int affected=cmd.ExecuteNonQuery();
                con.Close();
                if(affected > 0)
                {
                    return "Updated Successfully";
                }else { return null; }

            }
            //return null;
        }






        public TokenEmailClass ForgetPassword(string Email)
        {
            using(SqlConnection con=new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("cp_Get_BookStoreUserByEmail", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@Email", Email);
                SqlDataReader reader = cmd2.ExecuteReader();
                
                TokenEmailClass model = new TokenEmailClass();
                while (reader.Read())
                {

                    model.Id = Convert.ToInt32(reader[0].ToString());
                    model.FullName = reader[1].ToString();
                    model.Email = reader[2].ToString();
                    model.Token = GenerateToken(Convert.ToInt32(reader[0].ToString()), reader[2].ToString());
                }
                con.Close();
                return model;
            }
            return null;
        }


        public bool ResetPasswordMethod(ResetPassword resetPassword)
        {
            using(SqlConnection con= new SqlConnection(connString))
            {
                con.Open();
                
                if(resetPassword.NewPassword.Equals(resetPassword.ConfirmPassword))
                {
                    string Password = EncryptPassword(resetPassword.ConfirmPassword);
                    SqlCommand cmd = new SqlCommand($"update BookStoreUser set UserPassword='{Password}' where Email='{resetPassword.Email}'", con);
                    int affected= cmd.ExecuteNonQuery();
                    con.Close();
                    if (affected > 0)
                    {
                        return true;
                    }
                }
                else return false;
                
            }
            return false;
        }


    }
}
