using SearchFight.Model;
using SearchFight.Utilities;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using SearchFight.Model.BingModel;

namespace SearchFight.Service.Impl
{
    public class SearchAzureApi : ISearch
    {
        private readonly string _urlApiBing          = ConfigurationManager.AppSettings["UrlApiBing"];
        private readonly string _keyApiBing          = ConfigurationManager.AppSettings["keyApiBing"];
        private readonly string _keyLabelApiBing     = ConfigurationManager.AppSettings["keyLabelApiBing"];
        private readonly string _searchEngineLabel   = ConfigurationManager.AppSettings["Searcher2Label"];

        public async Task<SearchResult> Search(string term)
        {
            string urlPath = $"{_urlApiBing}?q={term}";
            var headers     = new List<string[]> { new[] { _keyLabelApiBing, _keyApiBing } };
            var response    = await ResponseService.GetAsync(urlPath, headers);
            return GetMsnSearchResult(response);
        }

        public SearchResult GetMsnSearchResult(string result)
        {
            var modelBing = result.DeserializeJson<ResponseBingModel>();
            return new SearchResult
            {
                SearchEngine = _searchEngineLabel,
                TotalResults = modelBing.WebPages.TotalEstimatedMatches
            };
        }
    }
}