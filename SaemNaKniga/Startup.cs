using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaemNaKniga.Startup))]
namespace SaemNaKniga
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
