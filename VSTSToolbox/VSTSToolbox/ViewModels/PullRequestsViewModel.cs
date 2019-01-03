using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using Syncfusion.XlsIO;
using VSTSToolbox.API.VSTS;

namespace VSTSToolbox.ViewModels
{
    public class PullRequestsViewModel : ViewModelBase, IPageLifeCycleAwareViewModel
    {
        private readonly IVSTSService _vstsService;
        private readonly MainViewModel _mainViewModel;
        private DateTime _from;
        private DateTime _to;

        public ICommand ExportPullRequestsCommand { get; set; }

        public DateTime From
        {
            get => _from;
            set
            {
                if (value == _from)
                    return;
                _from = value;
                RaisePropertyChanged();
            }
        }

        public DateTime To
        {
            get => _to;
            set
            {
                if (value == _to)
                    return;
                _to = value;
                RaisePropertyChanged();
            }
        }

        public PullRequestsViewModel(IVSTSService vstsService, MainViewModel mainViewModel)
        {
            _vstsService = vstsService;
            _mainViewModel = mainViewModel;
            ExportPullRequestsCommand = new RelayCommand(async () => await ExportPullRequestsExecute());
            From = DateTime.Now;
            To = DateTime.Now;
        }

        public async Task ExportPullRequestsExecute()
        {
            try
            {
                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    //Instantiate the Excel application object
                    var application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel2013;
                    var workbook = application.Workbooks.Create(1);
                    var worksheet = workbook.Worksheets[0];
                    worksheet.Range[1, 1].Text = "Commit Id";
                    worksheet.Range[1, 2].Text = "PullRequest Id";
                    worksheet.Range[1, 3].Text = "Comment from Code Reviewer";
                    worksheet.Range[1, 4].Text = "Commit Link";
                   

                    var target = _vstsService.GetRepositoryTarget(_mainViewModel.SelectedCollection.name, _mainViewModel.SelectedProject.Name, _mainViewModel.SelectedRepository.Name);
                    int count = 0;
                    var completedPullRequests = await _vstsService.GetPullRequests(target, "completed", _mainViewModel.SelectedBranch.Name);
                    var doneCommits = await _vstsService.GetCommits(target, From, To, targetName: _mainViewModel.SelectedBranch.Name.Substring(_mainViewModel.SelectedBranch.Name.LastIndexOf("/", StringComparison.Ordinal)+1));

                    int row = 2;
                    foreach (var item in doneCommits.value)
                    {
                        worksheet.Range[row, 1].Text = item.commitId;
                        worksheet.Range[row, 4].Text = item.remoteUrl;
                        var pR = completedPullRequests.value.FirstOrDefault(x => x.lastMergeTargetCommit.commitId == item.commitId);

                        if (pR != null) 
                        {
                            worksheet.Range[row, 1].CellStyle.Color = Color.Green;
                            worksheet.Range[row, 2].Text = pR.pullRequestId.ToString();
                        }
                        else
                        {
                            worksheet.Range[row, 1].CellStyle.Color = Color.Red;
                        }

                        row++;
                    }

                    worksheet.AutofitColumn(1);
                    worksheet.AutofitColumn(2);
                    worksheet.AutofitColumn(3);
                    worksheet.AutofitColumn(4);
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        workbook.SaveAs(saveFileDialog.FileName + ".xls");
                        MessageBox.Show("Successfully saved!");
                    }

                    Debug.WriteLine("##########  " + count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void OnAppearing()
        {
        }

        public void OnDisappearing()
        {
        }
    }
}