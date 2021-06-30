using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Escape.Startup))]
namespace Escape
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
