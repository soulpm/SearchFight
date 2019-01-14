using SearchFight.Utilities;
using System.Configuration;
using System.Threading.Tasks;
using SearchFight.Model;

namespace SearchFight.Service.Impl
{
    public class SearchGoogleApi : ISearch
    {
        private readonly string _urlGoogleApi        = ConfigurationManager.AppSettings["urlApiGoogle"];
        private readonly string _cxGoogleApi         = ConfigurationManager.AppSettings["cxApiGoogle"];
        private readonly string _keyGoogleApi        = ConfigurationManager.AppSettings["keyApiGoogle"];
        private readonly string _searchEngineLabel   = ConfigurationManager.AppSettings["Searcher1Label"];

        public async Task<SearchResult> Search(string term)
        {
            var urlPath = $"{_urlGoogleApi}?q={term}&cx={_cxGoogleApi}&key={_keyGoogleApi}";
            var response = await ResponseService.GetAsync(urlPath);
            return GetGoogleSearchValueModel(response);
        }
        public SearchResult GetGoogleSearchValueModel(string result)
        {
            var modelGoogle = result.DeserializeJson<ResponseGoogleModel>();
            return new SearchResult
            {
                SearchEngine = _searchEngineLabel,
                TotalResults = modelGoogle.SearchInformation.TotalResults
            };
        }
    }
}