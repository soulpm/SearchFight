using System.Threading.Tasks;
using SearchFight.Model;

namespace SearchFight.Service
{
    public interface ISearch
    {
        Task<SearchResult> Search(string Term);
    }
}