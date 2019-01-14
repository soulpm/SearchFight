using FluentAssertions;
using NUnit.Framework;
using SearchFight.Utilities;
using System;

namespace SearchFight.Tests
{
    [TestFixture]
    class TestValidation
    {
        
        [Test]
        public void GetArgumentsNotEmpt_OkStatus_ShouldGenerateArrayValid()
        {
            var query = new String[] { "java"," .net\"","\"java script\"" };
            var result = Validation.GetArgumentsNotEmpty(query);
            result.GetType().Should().Be(typeof(string []));
        }
    }
}
