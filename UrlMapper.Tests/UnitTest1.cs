using System;
using System.Collections.Generic;
using UrlMapper;
using Xunit;

namespace UrlMapper.Tests {
    public class UnitTest1 {

        [Theory]
        [InlineData ("https://mana.com/linkto/{link-id}", "https://mana.com/linkto/A2752348", "A2752348")]
        public void ManaLinkTo (string patternUrl, string tragetUrl, string expectedResult) {
            var builder = new SimpleStringParameterBuilder ();

            var dd = builder.Parse (patternUrl);
            bool ismatch = dd.IsMatched (tragetUrl);
            Assert.True (ismatch);
            // var dic = new Dictionary<string, string> ();

            // dd.ExtractVariables (tragetUrl, dic);
            // Assert.Equal (expectedResult, dic["{link-id}"]);
        }
    }
}