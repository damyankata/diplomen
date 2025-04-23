using OfficeShop.Core.Contracts;
using OfficeShop.Data;
using OfficeShop.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeShop.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(int productId, string userId, int quantity)
        {
            // намиране на продукта
            var product = this._context.Products.SingleOrDefault(x => x.Id == productId);

            if (product == null)
            {
                return false;
            }

            // създаване на поръчка
            Order item = new Order
            {
                OrderDate = DateTime.Now,
                ProductId = productId,
                UserId = userId,
                Quantity = quantity,
                Price = product.Price,
                Discount = product.Discount
            };

            // намаляване наличното количество
            product.Quantity -= quantity;
            _context.Products.Update(product);

            // записване на поръчката
            this._context.Orders.Add(item);
            return _context.SaveChanges() != 0;
        }

        public List<Order> GetOrders()
        {
            return _context.Orders
            .OrderByDescending(x => x.OrderDate)
            .ToList();
        }

        public List<Order> GetOrdersByUser(string userId)
        {
            return _context.Orders
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.OrderDate)
                .ToList();
        }

    }
}
