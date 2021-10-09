using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EClaim.Startup))]
namespace EClaim
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
