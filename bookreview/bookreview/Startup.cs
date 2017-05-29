using Microsoft.Owin;
using Owin;
using bookreview.Models;
using bookreview.Data;

[assembly: OwinStartupAttribute(typeof(bookreview.Startup))]
namespace bookreview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ApplicationDbContext context = new ApplicationDbContext();
            DbInitializer.Initialize(context);
        }
    }
}
