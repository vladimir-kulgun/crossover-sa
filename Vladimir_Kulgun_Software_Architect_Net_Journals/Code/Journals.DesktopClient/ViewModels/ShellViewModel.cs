using Prism.Modularity;
using Prism.Regions;

namespace Journals.DesktopClient.ViewModels
{
    public class ShellViewModel
    {
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel" /> class.
        /// </summary>
        /// <param name="moduleManager">The module manager.</param>
        /// <param name="regionManager">The region manager.</param>
        public ShellViewModel(IModuleManager moduleManager, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            moduleManager.LoadModuleCompleted += delegate { NavigateToPinScreen(); };
        }

        /// <summary>
        ///     Navigates to the pin screen.
        /// </summary>
        private void NavigateToPinScreen()
        {
            _regionManager.RequestNavigate(RegionName.MainRegion, ViewName.LoginView);
        }
    }
}