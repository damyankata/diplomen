using OfficeShop.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeShop.Core.Contracts
{
    public interface IOrderService
    {
        bool Create(int productId, string userId, int quantity);

        List<Order> GetOrders();
        List<Order> GetOrdersByUser(string userId);
        // Може да добавиш и тези после при нужда:
        /*
        Order GetOrderById(int orderId);
        bool RemoveById(int orderId);
        bool Update(int orderId, int productId, string userId, int quantity);
        */
    }
}
