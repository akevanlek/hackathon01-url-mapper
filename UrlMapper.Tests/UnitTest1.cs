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
        public void MappingUrl_OneParameter_successed (string patternUrl, string targetUrl, string key, string expectedResult) {
            var builder = new SimpleStringParameterBuilder ();
            var result = builder.Parse (patternUrl);
            bool isMatch = result.IsMatched (targetUrl);
            Assert.True (isMatch);

            var dic = new Dictionary<string, string> ();
            result.ExtractVariables (targetUrl, dic);
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
        public void MappingUrl_OneParameter_failed (string patternUrl, string targetUrl) {
            var builder = new SimpleStringParameterBuilder ();
            var result = builder.Parse (patternUrl);
            bool isMatch = result.IsMatched (targetUrl);
            Assert.False (isMatch);
        }

        [Theory]
        [InlineData ("https://mana.com/app/{app-id}/services/{service-id}", "https://mana.com/app/di394/services/878", new [] { "{app-id}", "{service-id}" }, new [] { "di394", "878" })]
        [InlineData ("https://mana.com/nana/{app/-id}/services/{service-id}", "https://mana.com/nana/di394/services/services/878", new [] { "{app/-id}", "{service-id}" }, new [] { "di394", "services/878" })]
        public void MappingUrl_ManayParameter_suceessed (string patternUrl, string targetUrl, string[] key, string[] expectedResult) {

            var builder = new SimpleStringParameterBuilder ();
            var result = builder.Parse (patternUrl);
            bool isMatch = result.IsMatched (targetUrl);
            Assert.True (isMatch);

            var dic = new Dictionary<string, string> ();
            result.ExtractVariables (targetUrl, dic);

            for (int index = 0; index < key.Length; index++) {
                Assert.True (dic.ContainsKey (key[index]));
                Assert.Equal (expectedResult[index], dic[key[index]]);
            }
        }

    }
}