using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IOrderRL
    {
        public OrderModel AddOrder(OrderModel order, int userId);
        public List<OrderModel> GetAllOrder(int userId);
    }
}
