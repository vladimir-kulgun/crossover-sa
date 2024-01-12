using Journals.DesktopClient.Views;
using Prism.Modularity;
using Prism.Regions;

namespace Journals.DesktopClient
{
    public class MainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(ViewName.LoginView, typeof(LoginView));
            _regionManager.RegisterViewWithRegion(ViewName.SubscriptionsView, typeof (SubscriptionsView));
            _regionManager.RegisterViewWithRegion(ViewName.ContentView, typeof(ContentView));
        }
    }
}