using Autofac;
using Database;
using Services;

namespace Shop
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShopContext>().AsSelf();
            builder.RegisterModule<ServicesModule>();
        }
    }
}
