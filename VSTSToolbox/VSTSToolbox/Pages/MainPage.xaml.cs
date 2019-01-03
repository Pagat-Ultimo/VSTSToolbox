using System.Windows;
using VSTSToolbox.ViewModels;

namespace VSTSToolbox.Pages
{
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
            PullRequestTab.DataContext = App.Locator.PullRequestsViewModel;
            DataContext = App.Locator.MainViewModel;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            { if(PullRequestTab.DataContext is IPageLifeCycleAwareViewModel viewModel)
                viewModel.OnAppearing(); }
            { if (DataContext is IPageLifeCycleAwareViewModel viewModel)
                viewModel.OnAppearing(); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new LicencesPage();
            dialog.Show();
        }
    }
}
