using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quejas_Proyecto.Startup))]
namespace Quejas_Proyecto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
