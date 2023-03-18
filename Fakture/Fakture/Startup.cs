using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fakture.Startup))]
namespace Fakture
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
