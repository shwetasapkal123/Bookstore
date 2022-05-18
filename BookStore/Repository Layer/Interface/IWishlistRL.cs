using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IWishlistRL
    {
        public string AddInWishlist(int bookId, int userId);
        public bool DeleteFromWishlist(int userId, int wishlistId);
        public List<WishlistModel> GetAllFromWishlist(int userId);
    
    }
}
