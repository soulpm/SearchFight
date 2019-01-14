using System.Collections.Generic;
using SearchFight.Service.Impl;

namespace SearchFight.Service
{
    public class SearchClientFactory
    {
        public static List<ISearch> CreateSearchEngines() =>
            new List<ISearch>
            {
                new SearchGoogleApi(),
                new SearchAzureApi()
            };
    }
}