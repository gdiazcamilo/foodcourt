using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(foodcourt.Startup))]
namespace foodcourt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
