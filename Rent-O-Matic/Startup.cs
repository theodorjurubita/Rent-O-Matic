using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rent_O_Matic.Startup))]
namespace Rent_O_Matic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
