using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PatientManagementApp.Startup))]
namespace PatientManagementApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
