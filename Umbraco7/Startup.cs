using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Umbraco7.Startup))]
namespace Umbraco7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
