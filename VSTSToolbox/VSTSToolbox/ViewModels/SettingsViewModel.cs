using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using VSTSToolbox.Models;

namespace VSTSToolbox.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IPageLifeCycleAwareViewModel
    {
        private readonly SettingsModel _settingsModel;
        private string _tfsUrl;
        private string _pat;

        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string TfsUrl
        {
            get => _tfsUrl;
            set
            {
                if (value == _tfsUrl)
                    return;
                _tfsUrl = value;
                RaisePropertyChanged();
            }
        }

        public string PAT
        {
            get => _pat;
            set
            {
                if (value == _pat)
                    return;
                _pat = value;
                RaisePropertyChanged();
            }
        }

        public SettingsViewModel(SettingsModel settingsModel)
        {
            _settingsModel = settingsModel;
            OkCommand = new RelayCommand(OkExecute);
            CancelCommand = new RelayCommand(CancelExecute);
        }

        private void OkExecute()
        {
            _settingsModel.PAT = PAT;
            _settingsModel.TfsUrl = TfsUrl;
            _settingsModel.SaveSettings();
        }

        private void CancelExecute()
        {
        }

        public void OnAppearing()
        {
            _settingsModel.LoadSettings();
            TfsUrl = _settingsModel.TfsUrl;
            PAT = _settingsModel.PAT;
        }

        public void OnDisappearing()
        {
        }
    }
}
