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
    public class BookRL:IBookRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        

        public BookModel AddBook(BookModel book)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("AddBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@bookName", book.BookName);
                cmd.Parameters.AddWithValue("@authorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@rating", book.Rating);
                cmd.Parameters.AddWithValue("@totalview", book.TotalView);
                cmd.Parameters.AddWithValue("@originalPrice", book.OriginalPrice);
                cmd.Parameters.AddWithValue("@discountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("@bookdetails", book.BookDetails);
                cmd.Parameters.AddWithValue("@bookImage", book.BookImage);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return book;
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

        public UpdateBook UpdateBook(UpdateBook update)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStore"]);
                SqlCommand com = new SqlCommand("UpdateBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@BookId", update.BookId);
                com.Parameters.AddWithValue("@BookName", update.BookName);
                com.Parameters.AddWithValue("@AuthorName", update.AuthorName);
                com.Parameters.AddWithValue("@OriginalPrice", update.OriginalPrice);
                com.Parameters.AddWithValue("@DiscountPrice", update.DiscountPrice);
                com.Parameters.AddWithValue("@BookDetails", update.BookDetails);
                com.Parameters.AddWithValue("@BookImage", update.BookImage);

                this.sqlConnection.Open();
                int i = com.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return update;
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
        public bool DeleteBook(int bookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("DeleteBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@bookId", bookId);
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

        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BooKStore"]);
                SqlCommand cmd = new SqlCommand("GetBookByBookId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookId", BookId);
                this.sqlConnection.Open();
                BookModel bookModel = new BookModel();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bookModel.BookName = reader["BookName"].ToString();
                        bookModel.AuthorName = reader["AuthorName"].ToString();
                        bookModel.Rating = Convert.ToInt32(reader["Rating"]);
                        bookModel.TotalView = Convert.ToInt32(reader["TotalView"]);
                        bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        bookModel.BookDetails = reader["BookDetails"].ToString();
                        bookModel.BookImage = reader["BookImage"].ToString();
                    }

                    this.sqlConnection.Close();
                    return bookModel;
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
        public List<BookModel> GetAllBooks()
        {
            try
            {
                List<BookModel> book = new List<BookModel>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                SqlCommand cmd = new SqlCommand("GetAllBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Add(new BookModel
                        {
                            BookId = Convert.ToInt32(reader["bookId"]),
                            BookName = reader["bookName"].ToString(),
                            AuthorName = reader["authorName"].ToString(),
                            Rating = Convert.ToInt32(reader["rating"]),
                            //Rating = Convert.ToInt32(reader["totalRating"]),
                            DiscountPrice = Convert.ToDecimal(reader["discountPrice"]),
                            OriginalPrice = Convert.ToDecimal(reader["originalPrice"]),
                            BookDetails = reader["BookDetails"].ToString(),
                            BookImage = reader["bookImage"].ToString(),
                            //BookCount = Convert.ToInt32(reader["BookCount"])
                        });
                    }

                    this.sqlConnection.Close();
                    return book;
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
