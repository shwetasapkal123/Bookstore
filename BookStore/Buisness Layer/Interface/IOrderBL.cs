using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Interface
{
    public interface IOrderBL
    {
        public OrderModel AddOrder(OrderModel order, int userId);
        public List<OrderModel> GetAllOrder(int userId);
    }
}
