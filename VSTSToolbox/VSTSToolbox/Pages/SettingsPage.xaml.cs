using System.Windows;
using VSTSToolbox.ViewModels;

namespace VSTSToolbox.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Window
    {
        public SettingsPage()
        {
            InitializeComponent();
            DataContext = App.Locator.SettingsViewModel;
            Loaded += SettingsPage_Loaded;
        }
        private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            {
                if (DataContext is IPageLifeCycleAwareViewModel viewModel)
                    viewModel.OnAppearing();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
