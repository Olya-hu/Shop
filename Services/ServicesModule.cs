using Autofac;
using Services.Catalog;
using Services.DbConnection;
using Services.Orders;

namespace Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CatalogService>().As<ICatalog>();
            builder.RegisterType<OrdersService>().As<IOrders>();
            builder.RegisterType<ShopConnection>().As<IShopConnection>().SingleInstance();
        }
    }
}
