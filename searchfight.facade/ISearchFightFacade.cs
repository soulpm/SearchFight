using SearchFight.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace searchfight.facade
{
    public interface ISearchFightFacade
    {
        string _messageResponse { get; set; }
        List<SearchResult> InitSearch(IEnumerable<string> arguments);
        Task<List<SearchResult>> GetResultsAsync(IEnumerable<string> querys);
        string GetReportSearchEngine(SearchResult searcherValue);
        string GetReportWinner(List<SearchResult> totalResults,SearchResult totalWinner);
        List<SearchResult> GetProcessWinners(List<SearchResult> results);
        SearchResult GetProcessTotalWinner(List<SearchResult> results);
        void PrintResults(List<SearchResult> results, List<SearchResult> totalResults,SearchResult totalWinner);
    }
}
