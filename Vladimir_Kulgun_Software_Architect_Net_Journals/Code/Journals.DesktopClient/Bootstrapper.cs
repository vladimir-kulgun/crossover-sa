using System.Windows;
using Autofac;
using Journals.DesktopClient.Services;
using Prism.Autofac;
using Prism.Modularity;

namespace Journals.DesktopClient
{
    public class Bootstrapper : AutofacBootstrapper
    {
        /// <summary>
        ///     Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application. </returns>
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Views.Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Views.Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        ///     Creates the <see cref="T:Prism.Modularity.IModuleCatalog" /> used by Prism.
        /// </summary>
        /// <returns>Returns a new ModuleCatalog.</returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            catalog.AddModule(typeof(MainModule));
            return catalog;
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);

            builder.RegisterType<Views.Shell>();

            // register views
            builder.RegisterTypeForNavigation<Views.LoginView>(ViewName.LoginView);
            builder.RegisterTypeForNavigation<Views.SubscriptionsView>(ViewName.SubscriptionsView);
            builder.RegisterTypeForNavigation<Views.ContentView>(ViewName.ContentView);

            // register services
            builder.RegisterType<SubscriberServiceProxy>().As<ISubscriberService>();
            builder.RegisterType<Settings>().As<ISettings>();
            builder.RegisterType<UserSettings>().As<IUserSettings>().SingleInstance();
        }
    }
}