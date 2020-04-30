using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Enteprise_programming_evita.Startup))]
namespace Enteprise_programming_evita
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
