using System.Windows.Input;
using Journals.DesktopClient.Services;
using Prism.Commands;
using Prism.Regions;

namespace Journals.DesktopClient.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IUserSettings _userSettings;
        private readonly IRegionManager _regionManager;

        public LoginViewModel(ISubscriberService subscriberService, IUserSettings userSettings, IRegionManager regionManager)
        {
            _subscriberService = subscriberService;
            _userSettings = userSettings;
            _regionManager = regionManager;
            LoginCommand = new DelegateCommand(Login);
        }

        public ICommand LoginCommand { get; set; }

        public string UserName { get; set; }

        private async void Login()
        {
            _userSettings.UserId = await _subscriberService.Login(UserName);
            _userSettings.UserName = UserName;

            // navigate to actions
            _regionManager.RequestNavigate(RegionName.MainRegion, ViewName.SubscriptionsView);
        }
    }
}