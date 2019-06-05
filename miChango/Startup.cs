using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(miChango.Startup))]
namespace miChango
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
