using System;
using System.Collections.Generic;
using System.Linq;
using UrlMapper;
using Xunit;

namespace UrlMapper.Tests {
    public class UnitTest1 {

        [Theory]
        [InlineData ("https://mana.com/linkto/{link-id}", "https://mana.com/linkto/A2752348", "{link-id}", "A2752348")]
        [InlineData ("https://mana.com/linkto/{link-id}", "https://mana.com/linkto//A2752348", "{link-id}", "/A2752348")]
        [InlineData ("http://google.com/?s={keyword}", "http://google.com/?s=A2752348", "{keyword}", "A2752348")]
        [InlineData ("http://google.com/?s={keyword}", "http://google.com/?s=/A2752348", "{keyword}", "/A2752348")]
        public void MappingUrl_OneParameter_successed (string patternUrl, string tragetUrl, string key, string expectedResult) {
            var builder = new SimpleStringParameterBuilder ();

            var result = builder.Parse (patternUrl);
            bool isMatch = result.IsMatched (tragetUrl);
            Assert.True (isMatch);

            var dic = new Dictionary<string, string> ();
            result.ExtractVariables (tragetUrl, dic);
            Assert.True (dic.ContainsKey (key));
            Assert.Equal (expectedResult, dic[key]);
        }

        [Theory]
        [InlineData ("https://mana.com/linkto/{link-id}", "https://mana.com/linkt/A2752348")]
        [InlineData ("https://mana.com/linkto/{link-id}", "https://mana.comlinkto/A2752348")]
        [InlineData ("https://mana.com/linkto/{link-id}", "https://manacom/linkto/A2752348")]
        [InlineData ("https://mana.com/linkto/{link-id}", "https:/mana.com/linkto/A2752348")]
        [InlineData ("https://mana.com/linkto/{link-id}", "https//mana.com/linkto/A2752348")]
        [InlineData ("https://mana.com/linkto/{link-id}", "http://mana.com/linkto/A2752348")]
        [InlineData ("http://google.com/?s={keyword}", "http://google.com/?sA2752348")]
        [InlineData ("http://google.com/?s={keyword}", "http://google.com/s=A2752348")]
        [InlineData ("http://google.com/?s={keyword}", "http://google.com?s=A2752348")]
        [InlineData ("http://google.com/?s={keyword}", "http://googlecom/?s=A2752348")]
        [InlineData ("http://google.com/?s={keyword}", "http:/google.com/?s=A2752348")]
        [InlineData ("http://google.com/?s={keyword}", "http//google.com/?s=A2752348")]
        public void MappingUrl_OneParameter_failed (string patternUrl, string tragetUrl) {
            var builder = new SimpleStringParameterBuilder ();

            var result = builder.Parse (patternUrl);
            bool isMatch = result.IsMatched (tragetUrl);
            Assert.False (isMatch);
        }
    }
}