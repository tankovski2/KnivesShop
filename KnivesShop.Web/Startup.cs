using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KnivesShop.Web.Startup))]
namespace KnivesShop.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
