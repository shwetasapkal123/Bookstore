using System;
using System.Collections.Generic;
using System.Text;

namespace Database_Layer
{
    public class CartModel
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public BookModel Bookmodel { get; set; }
    }
}
