using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SearchPOC.Startup))]
namespace SearchPOC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
