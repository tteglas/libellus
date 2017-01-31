using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Libellus.Startup))]
namespace Libellus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
