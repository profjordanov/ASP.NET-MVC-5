using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearningSystem.Web.Startup))]
namespace LearningSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
