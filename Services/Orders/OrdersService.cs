using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.EntityFrameworkCore;
using Services.DbConnection;
using Services.Orders.Requests;

namespace Services.Orders
{
    class OrdersService : IOrders
    {
        private readonly ShopContext _dbContext;

        public OrdersService(IShopConnection shopConnection)
        {
            _dbContext = shopConnection.Context;
        }

        public async Task<List<Shipping>> GetShippings()
        {
            return await _dbContext.Shipping.ToListAsync();
        }

        public async Task Make(OrderRequest request)
        {
            if ((await _dbContext.User.FindAsync(request.UserId)).Postcode == null)
                throw new ArgumentNullException("Отсутствует почтовый индекс");
            var payment = await _dbContext.Product.Where(product => request.ProductIds.Contains(product.Id))
                              .SumAsync(product => product.Price)
                          + (await _dbContext.Shipping.FindAsync(request.ShippingId)).Price;
            var order = await _dbContext.Order.AddAsync(new Order
            {
                UserId = request.UserId,
                ShippingId = request.ShippingId,
                Date = DateTime.Now,
                Payment = payment
            });
            await _dbContext.SaveChangesAsync();
            foreach (var productId in request.ProductIds)
            {
                await _dbContext.ProductOrder.AddAsync(new ProductOrder
                {
                    OrderId = order.Entity.Id,
                    ProductId = productId
                });
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}