using Buisness_Layer.Interface;
using Database_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Services
{
    public class OrderBL:IOrderBL
    {
        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }
        public OrderModel AddOrder(OrderModel order, int userId)
        {
            try
            {
                return this.orderRL.AddOrder(order, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<OrderModel> GetAllOrder(int userId)
        {
            try
            {
                return this.orderRL.GetAllOrder(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
