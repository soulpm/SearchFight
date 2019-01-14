using FluentAssertions;
using NUnit.Framework;
using SearchFight.Model;
using SearchFight.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Tests
{
    [TestFixture]
    class SearchApiTest
    {

        private SearchFightFacade _searchFacade;

        [SetUp]
        public void Init()
        {
            _searchFacade = new SearchFightFacade();
        }

        [Test]
        public void GetResultsAsync_WithNullQuerys_ShouldThrowArgumentNullException()
        {
            List<string> querys = null;
            Func<Task> result = async () => await _searchFacade.GetResultsAsync(querys);
            result.Should().ThrowExactly<ArgumentNullException>();
        }


        [Test]
        public async Task GetSearchReport_WithOkQuerys_ShouldGenerateReportAsString()
        {
            var querys = new List<string>() { ".net", "java" };
            var result = await _searchFacade.GetResultsAsync(querys);
            result.Should().BeOfType<List<SearchResult>>();
        }

        [Test]
        public async Task GetResultsAsync_OkSearchResults_ShouldNotBeEmpty()
        {
            var querys = new List<string> { ".net", "java" };

            var results = await _searchFacade.GetResultsAsync(querys);

            results.Should().NotBeEmpty();
        }

        [Test]
        public void GetWinners_WithNetAsGoogleWinner_GenerateWinner()
        {
            var searchResults = new List<SearchResult>
            {
                new SearchResult
                {
                    Term            = ".net",
                    SearchEngine    = "Google",
                    TotalResults    = 127000
                },
                new SearchResult
                {
                    Term            = ".net",
                    SearchEngine    = "MSN Search",
                    TotalResults    = 434600
                },
                new SearchResult
                {
                    Term            = "java",
                    SearchEngine    = "Google",
                    TotalResults    = 375600
                },
                new SearchResult
                {
                    Term            = "java",
                    SearchEngine    = "MSN Search",
                    TotalResults    = 112045
                },
            };

            var results = _searchFacade.GetProcessWinners(searchResults);
            results.Should().Contain(r => r.SearchEngine == "Google" && r.Term == ".net");
        }

        [Test]
        public void GetTotalWinner_WithPythonAsTotalWinner_ShouldGenerateWinnerString()
        {
            var searchResults = new List<SearchResult>
            {
                new SearchResult
                {
                    Term = ".net",
                    SearchEngine= "Google",
                    TotalResults = 17000
                },
                new SearchResult
                {
                    Term = ".net",
                    SearchEngine= "MSN Search",
                    TotalResults = 25000
                },
                new SearchResult
                {
                    Term = "python",
                    SearchEngine= "Google",
                    TotalResults = 29000
                },
                new SearchResult
                {
                    Term = "python",
                    SearchEngine= "MSN Search",
                    TotalResults = 38000
                },
            };

            var results = _searchFacade.GetProcessTotalWinner(searchResults);
            results.Should().Be(".python");
        }

        [Test]
        public void GetTotalWinner_WithNullSearchResults_ShouldThrowArgumentNullException()
        {
            List<SearchResult> searchResults = null;

            Action result = () => _searchFacade.GetProcessTotalWinner(searchResults);

            result.Should().ThrowExactly<ArgumentNullException>();
        }
        
    }
}
