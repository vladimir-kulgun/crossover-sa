using AutoMapper;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Journals.WebPortal.Startup))]

namespace Journals.WebPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            ConfigureAutofac(app);
            ConfigureAutoMapper();
        }
    }

}
