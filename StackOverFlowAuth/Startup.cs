using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StackOverFlowAuth.Startup))]
namespace StackOverFlowAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
