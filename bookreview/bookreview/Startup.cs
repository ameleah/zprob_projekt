using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bookreview.Startup))]
namespace bookreview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
