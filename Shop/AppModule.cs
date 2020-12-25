using Autofac;
using Services;

namespace Shop
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<ServicesModule>();
        }
    }
}
