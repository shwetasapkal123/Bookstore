using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface ICartRL
    {
        public CartModel AddCart(CartModel cart, int userId);
        public CartModel UpdateCart(CartModel cart, int userId);
        public bool DeleteCart(int cartId, int userId);
        public List<CartModel> GetCartByUserId(int userId);
    }
}
