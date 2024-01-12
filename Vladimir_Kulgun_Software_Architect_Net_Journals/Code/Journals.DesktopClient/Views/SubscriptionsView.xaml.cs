using System.Windows.Controls;
using Journals.DesktopClient.ViewModels;

namespace Journals.DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for SubscriptionsView.xaml
    /// </summary>
    public partial class SubscriptionsView : UserControl
    {
        public SubscriptionsView(SubscriptionsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
