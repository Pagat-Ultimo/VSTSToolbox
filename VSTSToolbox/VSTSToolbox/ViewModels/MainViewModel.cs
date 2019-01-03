using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using VSTSToolbox.API.Models;
using VSTSToolbox.API.VSTS;
using VSTSToolbox.Models;
using VSTSToolbox.Pages;

namespace VSTSToolbox.ViewModels
{
    public class MainViewModel : ViewModelBase, IPageLifeCycleAwareViewModel
    {
        private readonly IVSTSService _vstsService;
        private readonly SettingsModel _settingsModel;
        private readonly IVSTSServiceConfig _vstsServiceConfig;

        private CollectionModel _selectedCollection;
        private ProjectModel _selectedProject;
        private RepositoryModel _selectedRepository;
        private BranchModel _selectedBranch;

        public ICommand RefreshOrganizationCommand { get; set; }
        public ICommand ShowSettingsCommand { get; set; }
        public ObservableCollection<CollectionModel> AvailableCollections { get; set; } = new ObservableCollection<CollectionModel>();
        public ObservableCollection<ProjectModel> AvailableProjects { get; set; } = new ObservableCollection<ProjectModel>();
        public ObservableCollection<RepositoryModel> AvailableRepos { get; set; } = new ObservableCollection<RepositoryModel>();
        public ObservableCollection<BranchModel> AvailableBranches { get; set; } = new ObservableCollection<BranchModel>();

        public CollectionModel SelectedCollection
        {
            get => _selectedCollection;
            set
            {
                if (value == _selectedCollection)
                    return;
                _selectedCollection = value;
                RaisePropertyChanged();
                Task.Run(async () => await LoadProjects());
            }

        }

        public ProjectModel SelectedProject
        {
            get => _selectedProject;
            set
            {
                if (value == _selectedProject)
                    return;
                _selectedProject = value;
                RaisePropertyChanged();
                Task.Run(async () => await LoadRepositories());
            }
        }

        public RepositoryModel SelectedRepository
        {
            get => _selectedRepository;
            set
            {
                if (value == _selectedRepository)
                    return;
                _selectedRepository = value;
                RaisePropertyChanged();
                Task.Run(async () => await LoadBranches());
            }
        }

        public BranchModel SelectedBranch
        {
            get => _selectedBranch;
            set
            {
                if (value == _selectedBranch)
                    return;
                _selectedBranch = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel(IVSTSService vstsService, SettingsModel settingsModel, IVSTSServiceConfig vstsServiceConfig)
        {
            _vstsService = vstsService;
            _settingsModel = settingsModel;
            _vstsServiceConfig = vstsServiceConfig;
            RefreshOrganizationCommand = new RelayCommand(async () => await LoadOrganizations());
            ShowSettingsCommand = new RelayCommand(async () => await ShowSettings());
        }
        public async void OnAppearing()
        {
            try
            {
                var result = _settingsModel.LoadSettings();
                if (result == false)
                    MessageBox.Show(
                        "No saved settings found. Please open the settings page and configure at least the TfsUrl and the Personal Access Token");
                _vstsServiceConfig.BaseUrl = _settingsModel.TfsUrl;
                _vstsServiceConfig.PersonalAccessToken = _settingsModel.PAT;
                _vstsService.Initialize();
                await LoadOrganizations();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }

        }

        private async Task LoadOrganizations()
        {
            try
            {
                var collections = await _vstsService.GetAccountInfo();
                AvailableCollections = new ObservableCollection<CollectionModel>(collections);
                SelectedCollection = AvailableCollections?.FirstOrDefault();
                RaisePropertyChanged(nameof(AvailableCollections));
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading collections!", "Error");
            }
        }

        private async Task LoadProjects()
        {
            var collections = await _vstsService.GetProjectList(SelectedCollection.name);
            AvailableProjects = new ObservableCollection<ProjectModel>(collections);
            SelectedProject = AvailableProjects?.FirstOrDefault();
            RaisePropertyChanged(nameof(AvailableProjects));
        }

        private async Task LoadRepositories()
        {
            var collections = await _vstsService.GetRepositoryList(SelectedCollection.name, SelectedProject.Name);
            AvailableRepos = new ObservableCollection<RepositoryModel>(collections);
            SelectedRepository = AvailableRepos?.FirstOrDefault();
            RaisePropertyChanged(nameof(AvailableRepos));
        }

        private async Task LoadBranches()
        {
            var target = _vstsService.GetRepositoryTarget(SelectedCollection.name, SelectedProject.Name, SelectedRepository.Name);
            var collections = await _vstsService.GetBranchList(target);
            AvailableBranches = new ObservableCollection<BranchModel>(collections);
            SelectedBranch = AvailableBranches?.FirstOrDefault();
            RaisePropertyChanged(nameof(AvailableBranches));
        }

        private async Task ShowSettings()
        {
            var dialog = new SettingsPage();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _vstsServiceConfig.BaseUrl = _settingsModel.TfsUrl;
                    _vstsServiceConfig.PersonalAccessToken = _settingsModel.PAT;
                    _vstsService.Initialize();
                if (AvailableCollections == null || AvailableCollections.Count < 1)
                    await LoadOrganizations();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
            }
        }

        public void OnDisappearing()
        {
        }
    }
}
