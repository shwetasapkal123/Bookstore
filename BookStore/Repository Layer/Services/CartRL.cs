﻿using Database_Layer;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository_Layer.Services
{
    public class CartRL:ICartRL
    {
        private SqlConnection sqlConnection;
        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public CartModel AddCart(CartModel cart, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("AddCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@UserId", cart.UserId);
                cmd.Parameters.AddWithValue("@BookId", cart.BookId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return cart;
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

        public CartModel UpdateCart(CartModel cart, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("UpdateCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@BookId", cart.BookId);
                // cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CartId", cart.CartId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return cart;
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

        public bool DeleteCart(int cartId, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("DeleteCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CartId", cartId);
                // cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
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
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public List<CartModel> GetCartByUserId(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("GetCartbyUserId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    List<CartModel> cartmodel = new List<CartModel>();
                    while (reader.Read())
                    {
                        BookModel booksModel = new BookModel();
                        CartModel cart = new CartModel();

                        booksModel.BookName = reader["bookName"].ToString();
                        booksModel.AuthorName = reader["authorName"].ToString();
                        booksModel.OriginalPrice = Convert.ToDecimal(reader["originalPrice"]);
                        booksModel.DiscountPrice = Convert.ToDecimal(reader["discountPrice"]);
                        booksModel.BookImage = reader["bookImage"].ToString();
                        cart.UserId = Convert.ToInt32(reader["UserId"]);
                        cart.BookId = Convert.ToInt32(reader["BookId"]);
                        cart.CartId = Convert.ToInt32(reader["CartId"]);
                        // cart.Quantity = Convert.ToInt32(reader["Quantity"]);
                        cart.Bookmodel = booksModel;
                        cartmodel.Add(cart);
                    }

                    this.sqlConnection.Close();
                    return cartmodel;
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
    }
}
