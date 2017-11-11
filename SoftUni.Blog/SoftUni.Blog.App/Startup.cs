using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SoftUni.Blog.App.Startup))]
namespace SoftUni.Blog.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
