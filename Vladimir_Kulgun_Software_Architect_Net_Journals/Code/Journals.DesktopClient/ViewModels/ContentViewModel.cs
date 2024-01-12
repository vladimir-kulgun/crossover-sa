using System.IO;
using System.Windows.Input;
using Journals.DesktopClient.Services;
using PdfiumViewer;
using Prism.Commands;
using Prism.Regions;

namespace Journals.DesktopClient.ViewModels
{
    public class ContentViewModel : BaseViewModel
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IUserSettings _userSettings;
        private readonly IRegionManager _regionManager;
        private PdfDocument _document;

        public ContentViewModel(ISubscriberService subscriberService, IUserSettings userSettings,
            IRegionManager regionManager)
        {
            _subscriberService = subscriberService;
            _userSettings = userSettings;
            _regionManager = regionManager;

            BackCommand = new DelegateCommand(Back);
            LoadCommand = new DelegateCommand(Load);

            JournalName = _userSettings.JournalName;
        }

        public ICommand BackCommand { get; set; }

        public ICommand LoadCommand { get; set; }

        public ICommand ViewCommand { get; set; }

        public PdfDocument Document
        {
            get { return _document; }
            set
            {
                if (_document != value)
                {
                    _document = value;
                    OnPropertyChanged();
                }
            }
        }

        public string JournalName { get; set; }

        private void Back()
        {
            Document?.Dispose();
            _regionManager.RequestNavigate(RegionName.MainRegion, ViewName.SubscriptionsView);
        }

        private async void Load()
        {
            var content = await _subscriberService.GetJournal(_userSettings.UserId, _userSettings.JournalId);

            var stream = new MemoryStream(content);
            Document = PdfDocument.Load(stream);
        }
    }
}