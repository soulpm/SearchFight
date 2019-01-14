using SearchFight.Model;
using System.Threading.Tasks;

namespace SearchFight.Service.Interfaces
{
    public interface ISearchApis
    {
        Task<SearchResult> Search(string Term);
        
    }
}