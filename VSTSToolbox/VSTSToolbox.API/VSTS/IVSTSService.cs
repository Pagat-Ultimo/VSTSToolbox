using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VSTSToolbox.API.Models;

namespace VSTSToolbox.API.VSTS
{
    public interface IVSTSService
    {
        void Initialize();
        RepositoryTarget GetRepositoryTarget(string organization, string project, string repositoryId);
        Task<PullRequestsModel> GetPullRequests(RepositoryTarget target, string status = "open", string targetName = "master");
        Task<GitCommitsModel> GetCommits(RepositoryTarget target, DateTime from, DateTime to, string targetType = "branch", string targetName = "master");
        Task<GitCommitsModel> GetCommitsFromPullRequest(string pullRequestId, RepositoryTarget target);
        Task<List<CollectionModel>> GetAccountInfo();
        Task<List<ProjectModel>> GetProjectList(string organization);
        Task<List<RepositoryModel>> GetRepositoryList(string organization, string project);
        Task<List<BranchModel>> GetBranchList(RepositoryTarget target);

    }
}
