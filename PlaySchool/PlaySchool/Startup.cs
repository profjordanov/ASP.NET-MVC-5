using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlaySchool.Startup))]
namespace PlaySchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
