using Database_Layer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository_Layer.Services
{
    public class UserRL:IUserRL
    {
        private SqlConnection sqlConnection;
        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        //Method for user registration
        public UserModel Register(UserModel user)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                //Adding stored procedure name and connction
                SqlCommand com = new SqlCommand("UserRegister", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //Adding all parameters to stored proecedure
                com.Parameters.AddWithValue("@FullName", user.FullName);
                com.Parameters.AddWithValue("@Email", user.Email);
                com.Parameters.AddWithValue("@Password", user.Password);
                com.Parameters.AddWithValue("@Address",user.Address);
                com.Parameters.AddWithValue("@PhoneNumber", user.Phone);
                this.sqlConnection.Open();
                int i = com.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        //method for user login 
        public UserLogin Login(string Email, string Password)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand com = new SqlCommand("UserLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", Email);
                com.Parameters.AddWithValue("@Password", Password);
                this.sqlConnection.Open();
                SqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    int UserId = 0;
                    UserLogin user = new UserLogin();
                    while (rd.Read())
                    {
                        user.Email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                        user.FullName = Convert.ToString(rd["FullName"] == DBNull.Value ? default : rd["FullName"]);
                        UserId = Convert.ToInt32(rd["UserId"] == DBNull.Value ? default : rd["UserId"]);

                    }

                    this.sqlConnection.Close();
                    user.Token = this.GenerateJWTToken(Email, UserId);
                    return user;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        //Generating token after login
        public string GenerateJWTToken(string Email , int userId)
        {
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // payload
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "User"),
                new Claim("Email", Email),
                new Claim("Id", userId.ToString()),
            };

            // signature
                var token = new JwtSecurityToken(
                this.Configuration["Jwt:Issuer"],
                this.Configuration["Jwt:Issuer"],
                claims,
                //Token will expire after 1hour
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ForgotPassword(string email)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand com = new SqlCommand("UserForgotPassword", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", email);
                this.sqlConnection.Open();
                SqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    int userId = 0;
                    while (rd.Read())
                    {
                        email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                        userId = Convert.ToInt32(rd["UserId"] == DBNull.Value ? default : rd["UserId"]);
                    }

                    this.sqlConnection.Close();
                    var token = this.GenerateJWTToken(email, userId);
                    new MsmqModel().Sender(token);
                    return token;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword == confirmPassword)
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                    SqlCommand com = new SqlCommand("UserResetPassword", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.AddWithValue("@Email", email);
                    com.Parameters.AddWithValue("@Password", confirmPassword);
                    this.sqlConnection.Open();
                    int i = com.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}
