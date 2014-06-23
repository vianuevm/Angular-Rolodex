using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rolodex3.Startup))]
namespace Rolodex3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
