using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VSTSToolbox.API.Models;
using VSTSToolbox.API.RestService;

namespace VSTSToolbox.API.VSTS
{
    public class VSTSService : IVSTSService
    {
        private readonly IRestClient restClient;
        private readonly IVSTSServiceConfig vstsServiceConfig;


        public VSTSService(IRestClientFactory restClientFactory, IVSTSServiceConfig vstsServiceConfig)
        {
            this.restClient = restClientFactory.CreateRestClient();
            this.vstsServiceConfig = vstsServiceConfig;
        }


        public void Initialize()
        {
           // var personalaccesstoken = "j4z6ffwtmdmnk5buzy27nbi6q6rejegtf3dwywoulcw3wfeaw3va";
            if (string.IsNullOrEmpty(vstsServiceConfig.PersonalAccessToken) || string.IsNullOrEmpty(vstsServiceConfig.BaseUrl))
                throw new Exception("PAT and TFS Url must be configured!");
            var header = new Dictionary<string, string>();
            header.Add("content-type", "application/json");
            this.restClient.SetUp(vstsServiceConfig.BaseUrl);
            this.restClient.SetAuthHeader(new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", vstsServiceConfig.PersonalAccessToken)))));
        }

        public RepositoryTarget GetRepositoryTarget(string organization, string project, string repositoryId)
        {
            return new RepositoryTarget()
            {
                RepositoryId = repositoryId,
                ProjectId = project,
                Organization = organization
            };
        }

        public async Task<PullRequestsModel> GetPullRequests(RepositoryTarget target, string status = "open", string targetName = "master")
        {
            var response = await this.restClient.GetAsync(
                GetRepositoryApiUrl(target) + $"pullrequests?searchCriteria.status={status}&searchCriteria.targetRefName={targetName}&api-version=4.0");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<PullRequestsModel>(response.Body);
            return null;
        }

        public async Task<GitCommitsModel> GetCommits(RepositoryTarget target, DateTime from, DateTime to, string targetType = "branch", string targetName = "master")
        {
            var response = await this.restClient.GetAsync(
                GetRepositoryApiUrl(target) + $"commits?searchCriteria.itemVersion.versionType={targetType}&searchCriteria.itemVersion.version={targetName}&searchCriteria.toDate={to:M/d/yyyy HH:mm:ss}&searchCriteria.fromDate={from:M/d/yyyy HH:mm:ss}&api-version=4.0");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<GitCommitsModel>(response.Body);
            return null;
        }

        public async Task<GitCommitsModel> GetCommitsFromPullRequest(string pullRequestId, RepositoryTarget target)
        {
            var response = await this.restClient.GetAsync(
                GetRepositoryApiUrl(target) + $"pullrequests/{pullRequestId}/commits?api-version=4.0");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<GitCommitsModel>(response.Body);
            return null;
        }

        public async Task<List<CollectionModel>> GetAccountInfo()
        {
            var response = await this.restClient.GetAsync(
                $"_api/_common/GetCollections");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<CollectionResponseModel>(response.Body)?.__wrappedArray;
            return null;
        }

        public async Task<List<ProjectModel>> GetProjectList(string organization)
        {
            var response = await this.restClient.GetAsync(
                $"{organization}/_apis/projects?api-version=4.0");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ProjectResponseModel>(response.Body)?.value;
            return null;
        }

        public async Task<List<RepositoryModel>> GetRepositoryList(string organization, string project)
        {
            var response = await this.restClient.GetAsync(
                $"{organization}/{project}/_apis/git/repositories?api-version=4.0");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<RepositoryResponseModel>(response.Body)?.value;
            return null;
        }

        public async Task<List<BranchModel>> GetBranchList(RepositoryTarget target)
        {
            var response = await this.restClient.GetAsync(
                GetRepositoryApiUrl(target) + $"refs?api-version=4.0");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<BranchResponseModel>(response.Body)?.value;
            return null;
        }

        private string GetRepositoryApiUrl(RepositoryTarget target)
        {
            return $"{target.Organization}/{target.ProjectId}/_apis/git/repositories/{target.RepositoryId}/";
        }
    }
}