using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PointOfSalesFull.Startup))]
namespace PointOfSalesFull
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
