using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigitalLibrary.Web.Startup))]
namespace DigitalLibrary.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
