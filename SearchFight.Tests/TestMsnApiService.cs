using FluentAssertions;
using NUnit.Framework;
using SearchFight.Model;
using SearchFight.Service.Impl;
using System;
using System.Threading.Tasks;

namespace SearchFight.Tests
{
    [TestFixture]
    class TestMsnApiService
    {
        private SearchAzureApi msnServiceClient;

        [SetUp]
        public void Init()
        {
            msnServiceClient = new SearchAzureApi();
        }

        [Test]
        public async Task GetResultsAsync_OkStatus_ShouldGenerateSearchResultResponse()
        {
            var query = "java";
            var result = await msnServiceClient.Search(query);
            result.GetType().Should().Be(typeof(SearchResult));
        }

        [Test]
        public void GetResultsAsync_OkStatus_ShouldThrowArgumentNullException()
        {
            string query = null;

            Func<Task> result = async () => await msnServiceClient.Search(query);

            result.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
