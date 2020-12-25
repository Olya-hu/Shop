using Autofac;
using Services.Catalog;
using Services.Orders;

namespace Services
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CatalogService>().As<ICatalog>();
            builder.RegisterType<OrdersService>().As<IOrders>();
        }
    }
}
