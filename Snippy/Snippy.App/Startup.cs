using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Snippy.App.Startup))]
namespace Snippy.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
