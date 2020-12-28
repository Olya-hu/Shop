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

        public async Task<IEnumerable<Shipping>> GetShippings()
        {
            return await _dbContext.Shipping.ToListAsync();
        }

        public async Task Make(OrderRequest request)
        {
            if ((await _dbContext.User.FindAsync(request.UserId)).Postcode == null)
                throw new ArgumentNullException("Отсутствует почтовый индекс");

            var bagItems = await GetBagForUser(request.UserId);
            
            var payment = await _dbContext.Product.Where(product => bagItems.Any(bag => bag.ProductId == product.Id))
                              .SumAsync(product => product.Price)
                          + (await _dbContext.Shipping.FindAsync(request.ShippingId)).Price;
            
            var order = (await _dbContext.Order.AddAsync(new Order
            {
                UserId = request.UserId,
                ShippingId = request.ShippingId,
                Date = DateTime.Now,
                Payment = payment
            })).Entity;
            await _dbContext.SaveChangesAsync();

            foreach (var bagItem in bagItems)
            {
                await _dbContext.ProductOrder.AddAsync(new ProductOrder
                {
                    OrderId = order.Id,
                    ProductId = bagItem.ProductId,
                    ProductSize = bagItem.Size
                });
                await _dbContext.SaveChangesAsync();
                var productSize = await _dbContext.ProductSize.FindAsync(bagItem.ProductId, bagItem.Size);
                productSize.Quantity--;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddToBag(ProductSize ps, int userId)
        {
            await _dbContext.Bag.AddAsync(new Bag
            {
                UserId = userId,
                ProductId = ps.ProductId,
                Size = ps.Size
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Bag>> GetBagForUser(int userId)
        {
            return await _dbContext.Bag.Where(bag => bag.UserId == userId).ToListAsync();
        }
    }
}