using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScrumProjekt.Startup))]
namespace ScrumProjekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
