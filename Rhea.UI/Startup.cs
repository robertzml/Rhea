using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rhea.UI.Startup))]
namespace Rhea.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
