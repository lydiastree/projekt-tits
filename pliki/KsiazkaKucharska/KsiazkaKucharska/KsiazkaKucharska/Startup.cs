using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KsiazkaKucharska.Startup))]
namespace KsiazkaKucharska
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
