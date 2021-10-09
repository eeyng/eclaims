using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eClaim.Web.Startup))]
namespace eClaim.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          ConfigureAuth(app);
        }
    }
}
