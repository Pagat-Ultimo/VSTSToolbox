using GalaSoft.MvvmLight.Ioc;
using VSTSToolbox.ViewModels;

namespace VSTSToolbox.Services
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<PullRequestsViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        public PullRequestsViewModel PullRequestsViewModel => SimpleIoc.Default.GetInstance<PullRequestsViewModel>();
        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();
        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();
    }
}
