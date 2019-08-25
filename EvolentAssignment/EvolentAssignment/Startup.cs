using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EvolentAssignment.Startup))]
namespace EvolentAssignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
