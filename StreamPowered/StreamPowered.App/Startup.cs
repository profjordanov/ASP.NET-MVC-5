using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StreamPowered.App.Startup))]
namespace StreamPowered.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
