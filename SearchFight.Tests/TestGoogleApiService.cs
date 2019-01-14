using System;
using SearchFight.Service.Impl;
using System.Threading.Tasks;
using SearchFight.Model;
using NUnit.Framework;
using FluentAssertions;

namespace SearchFight.Tests
{
    [TestFixture]
    public class TestGoogleApiService
    {
        private SearchGoogleApi googleServiceClient;

        [SetUp]
        public void Init() {
            googleServiceClient = new SearchGoogleApi();
        }

        [Test]
        public async Task GetResultsAsync_OkStatus_ShouldGenerateSearchResultResponse()
        {
            var query = "java";
            var result = await googleServiceClient.Search(query);
            result.GetType().Should().Be(typeof(SearchResult));
        }

        [Test]
        public void GetResultsAsync_OkStatus_ShouldThrowArgumentNullException()
        {
            string query = null;

            Func<Task> result = async () => await googleServiceClient.Search(query);

            result.Should().ThrowExactly<ArgumentNullException>();
        }

    }
}
