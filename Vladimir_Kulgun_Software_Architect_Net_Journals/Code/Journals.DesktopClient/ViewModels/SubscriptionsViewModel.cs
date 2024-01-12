using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Journals.DesktopClient.Models;
using Journals.DesktopClient.Services;
using Prism.Commands;
using Prism.Regions;

namespace Journals.DesktopClient.ViewModels
{
    public class SubscriptionsViewModel : BaseViewModel
    {
        readonly IUserSettings _userSettings;
        private readonly ISubscriberService _subscriberService;
        private readonly IRegionManager _regionManager;

        public SubscriptionsViewModel(IUserSettings userSettings, ISubscriberService subscriberService, IRegionManager regionManager)
        {
            _userSettings = userSettings;
            _subscriberService = subscriberService;
            _regionManager = regionManager;

            Items = new ObservableCollection<Journal>();

            BackCommand = new DelegateCommand(() => _regionManager.RequestNavigate(RegionName.MainRegion, ViewName.LoginView));
            ViewCommand = new DelegateCommand(View);

            LoadCommand = new DelegateCommand(Load);
        }

        public ICommand BackCommand { get; set; }

        public ICommand LoadCommand { get; set; }

        public ICommand ViewCommand { get; set; }

        public ObservableCollection<Journal> Items { get; set; }

        public Journal Selected { get; set; }

        private async void Load()
        {
            Items.Clear();

            var journals = await _subscriberService.GetJournals(_userSettings.UserId);

            Items.AddRange(journals);

            Selected = Items.FirstOrDefault();
        }

        private void View()
        {
            _userSettings.JournalId = Selected.Id;

            _regionManager.RequestNavigate(RegionName.MainRegion, ViewName.ContentView);
        }
    }
}