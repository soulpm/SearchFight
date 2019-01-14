using SearchFight.Model;
using System.Threading.Tasks;
using SearchFight.Service.Interfaces;

namespace SearchFight.Service.Impl
{
    public class SearchApis : ISearchApis
    {
        private ISearch _searchEngines;
        
        public Task<SearchResult> Search(string Term) => _searchEngines.Search(Term);
    }
}