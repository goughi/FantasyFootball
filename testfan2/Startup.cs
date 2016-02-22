using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testfan2.Startup))]
namespace testfan2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
