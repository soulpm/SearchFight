using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchFight.Model;
using System.Configuration;
using searchfight.facade;

namespace SearchFight.Service
{
    public class SearchFightFacade: ISearchFightFacade
    {
        private List<ISearch> _clientsEngines => SearchClientFactory.CreateSearchEngines();
        private string searchEngine1Label = ConfigurationManager.AppSettings["Searcher1Label"];
        private string searchEngine2Label = ConfigurationManager.AppSettings["Searcher2Label"];
        public string _messageResponse { get; set; }
        public List<SearchResult> InitSearch(IEnumerable<string> arguments)
        {
            try
            {
                return GetResultsAsync(arguments).Result;
            }
            catch(Exception ex)
            {
                _messageResponse = ex.Message;
                return null;
            }
        }
        public async Task<List<SearchResult>> GetResultsAsync(IEnumerable<string> querys)
        {
            List<SearchResult>  results = new List<SearchResult>();
            foreach (string itemTerm in querys.Distinct())
            {
                foreach (ISearch searchItem in _clientsEngines)
                {
                    var response = await searchItem.Search(itemTerm);
                    results.Add(new SearchResult
                    {
                        Term = itemTerm,
                        SearchEngine = response.SearchEngine,
                        TotalResults = response.TotalResults
                    });
                }
            }
            return results;
        }
        public void PrintResults(List<SearchResult> results, List<SearchResult> totalResults,SearchResult totalWinner)
        {
            string termDistinct = string.Empty;
            results.ForEach(e =>
            {
                if (termDistinct != e.Term)
                {
                    Console.Write("\n");
                    Console.Write($"{e.Term}:\t"); termDistinct = e.Term;
                    Console.Write(GetReportSearchEngine(e));
                }
                else {
                    Console.Write(GetReportSearchEngine(e));
                }
            });
            Console.Write(GetReportWinner(totalResults,totalWinner));

        }
        public List<SearchResult>  GetProcessWinners(List<SearchResult> results)
        {
            List<SearchResult> totalResults = new List<SearchResult>() {
                results.Where(it=>it.SearchEngine== searchEngine1Label).
                OrderByDescending(item => item.TotalResults).First(),
                results.Where(it =>it.SearchEngine == searchEngine2Label).
                OrderByDescending(item => item.TotalResults).First()
            };
            return totalResults;
        }

        public SearchResult GetProcessTotalWinner(List<SearchResult> results)
        {
            return results.OrderByDescending(item => item.TotalResults).First();
        }

        public string GetReportSearchEngine(SearchResult searcherValue)
        {
            StringBuilder Message = new StringBuilder();
            Message.Append(searcherValue.SearchEngine);
            Message.Append(": ");
            Message.Append(searcherValue.TotalResults);
            Message.Append("\t");
            return Message.ToString();
        }

        public string GetReportWinner(List<SearchResult> totalResults,SearchResult totalWinner)
        {
            StringBuilder message = new StringBuilder();
            message.Append("\n");
            foreach (SearchResult item in totalResults)
            {
                message.Append(item.SearchEngine);
                message.Append(" winner: ");
                message.Append(item.Term);
                message.Append("\n");
            }
            message.Append("Total Winner: ");
            message.Append(totalWinner.Term);
            return message.ToString();
        }
        
    }
}