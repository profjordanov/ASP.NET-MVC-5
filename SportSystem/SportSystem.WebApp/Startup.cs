using Microsoft.Owin;

using SportSystem.WebApp;

[assembly: OwinStartup(typeof(Startup))]
namespace SportSystem.WebApp
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
