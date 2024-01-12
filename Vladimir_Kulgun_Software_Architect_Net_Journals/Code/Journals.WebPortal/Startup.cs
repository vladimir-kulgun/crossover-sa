using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Journals.Dom.Services;
using Journals.WebPortal;
using Journals.WebPortal.Services;
using Microsoft.Owin;
using Owin;

namespace Journals.WebPortal
{


    public partial class Startup
    {
        public void ConfigureAutofac(IAppBuilder app)
        {
            var configuration = GlobalConfiguration.Configuration;

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(configuration);
            ConfigureDependencies(builder);

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public static void ConfigureDependencies(ContainerBuilder builder)
        {
            //RegisterModules
            builder.RegisterModule(new AutofacServicesModule());

            builder.RegisterType<FileUploadService>().As<IFileUploadService>();
        }
    }
}
