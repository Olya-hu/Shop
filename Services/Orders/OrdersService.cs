using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Enums;
using Microsoft.EntityFrameworkCore;
using Services.DbConnection;

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

        public async Task Make(int userId, int shippingId)
        {
            var bagItems = await GetBagForUser(userId);
            decimal payment = 0;
            foreach (var bagItem in bagItems)
            {
                payment += (await _dbContext.Product.FindAsync(bagItem.ProductId)).Price;
            }

            payment += (await _dbContext.Shipping.FindAsync(shippingId)).Price;

            var order = (await _dbContext.Order.AddAsync(new Order
            {
                UserId = userId,
                ShippingId = shippingId,
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
                if (productSize.Quantity == 0)
                    _dbContext.ProductSize.Remove(productSize);
                await _dbContext.SaveChangesAsync();
            }
            
            _dbContext.Bag.RemoveRange(bagItems);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrders(int orderId = -1)
        {
            List<Order> orders = null;
            if (orderId == -1)
                orders = await _dbContext.Order.ToListAsync();
            else
                orders = new List<Order> { await _dbContext.Order.FindAsync(orderId) };
            foreach (var order in orders)
            {
                order.User = await _dbContext.User.FindAsync(order.UserId);
            }

            return orders;
        }

        public async Task<List<Order>> GetOrdersForUser(int userId)
        {
            return await _dbContext.Order.Where(order => order.UserId == userId).ToListAsync();
        }

        public async Task<Order> GetOrder(int orderId)
        {
            var order = await _dbContext.Order.FindAsync(orderId);
            if (order != null)
            {
                order.ProductOrder = await _dbContext.ProductOrder.Where(po => po.OrderId == order.Id).ToListAsync();
                foreach (var productOrder in order.ProductOrder)
                {
                    productOrder.Product =
                        await _dbContext.ProductSize.FindAsync(productOrder.ProductId, productOrder.ProductSize);
                    productOrder.Product.Product = await _dbContext.Product.FindAsync(productOrder.ProductId);
                }

                order.Shipping = await _dbContext.Shipping.FindAsync(order.ShippingId);
            }

            return order;
        }

        public async Task ChangeStatus(int orderId, Status status)
        {
            var order = await _dbContext.Order.FindAsync(orderId);
            order.Status = status;
            await _dbContext.SaveChangesAsync();
        }

        private async Task<List<Bag>> GetBagForUser(int userId)
        {
            return await _dbContext.Bag.Where(bag => bag.UserId == userId).ToListAsync();
        }
    }
}