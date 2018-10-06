using System;
using System.Collections.Generic;
using System.Linq;
using UrlMapper;
using Xunit;

namespace UrlMapper.Tests
{
    public class Unitest2
    {

        [Theory]
        [InlineData("https://mana.com/linkto/{link-id}", "https://mana.com/linkto/A2752348", "{link-id}", "A2752348")]
        [InlineData("http://google.com/?s={keyword}", "http://google.com/?s=A2752348", "{keyword}", "A2752348")]
        [InlineData("http://google.com/?s={keycs1word}", "http://google.com/?s=A27v/52348", "{keycs1word}", "A27v/52348")]
        [InlineData("https://mana.com/linkto/{link/*-id}", "https://mana.com/linkto/A2752348", "{link/*-id}", "A2752348")]
        [InlineData("https://mana.com/linkto/{link0-id}", "https://mana.com/linkto/A27/*2348", "{link0-id}", "A27/*2348")]
        [InlineData("http://google.com/?s={keyword}", "http://google.com/?s=A2752348", "{keyword}", "A2752348")]
        [InlineData("http://google.com/?//s={keyword}", "http://google.com/?//s=A27523485666", "{keyword}", "A27523485666")]
        [InlineData(@"http://google.com/?//s={keyrtktyktyjd[]rj4362#$#$%^%^%$word}", @"http://google.com/?//s=A275234*&%^&*(&*)($$$@@$$%$%_/85666", "{keyrtktyktyjd[]rj4362#$#$%^%^%$word}", "A275234*&%^&*(&*)($$$@@$$%$%_/85666")]
        [InlineData("http://google.com/as.as/?[][][]//s={keyword}", "http://google.com/as.as/?[][][]//s=5689+", "{keyword}", "5689+")]
        [InlineData("http://google.com/?s/56/฿หฟฟหฟดหดฟ={****keyword}", "http://google.com/?s/56/฿หฟฟหฟดหดฟ=A2752348", "{****keyword}", "A2752348")]
        [InlineData("http://google.com/n?s={keyword}", "http://google.com/n?s=A2752348", "{keyword}", "A2752348")]   
        [InlineData("http://google.com/n/t/n/tn/n/y/t?s=/n{keyword}", "http://google.com/n/t/n/tn/n/y/t?s=/nA2752348", "{keyword}", "A2752348")]    
        public void ManaLinkTo(string patternUrl, string tragetUrl, string key, string expectedResult)
        {
            var builder = new SimpleStringParameterBuilder();

            var result = builder.Parse(patternUrl);
            bool isMatch = result.IsMatched(tragetUrl);
            Assert.True(isMatch);

            var dic = new Dictionary<string, string>();
            result.ExtractVariables(tragetUrl, dic);
            Assert.True(dic.ContainsKey(key));
            Assert.Equal(expectedResult, dic[key]);
        }

        [InlineData("http://google.com/n?s={keyword}", "http://google.com/n?s=A2752348", "{keyword}", "A2752348")]
        [InlineData("http://hackathon.com/{username}/service/{service-id}", "http://hackathon.com/test123/service/svid456", "{username}", "test123"," {service-id}", "svid456")]
        [InlineData("http://hackathon.com/{sdgdsg}/service/{service-id}", "http://hackathon.com/test123/service/svid456", "{sdgdsg}", "test123"," {service-id}", "svid456")]
        [InlineData("http://hackathon.com/{sdgdsg}/service=/{service-id}", "http://hackathon.com/test123/service=/svid455556", "{sdgdsg}", "test123"," {service-id}", "svid455556")]
        [InlineData("http://hackathon.com/{sdgdsg}/service=/{service-id}/{service-id56}/as/{e-id56}", "http://hackathon.com/test123/service=/svid455556/222/as/asas", "{sdgdsg}", "test123"," {service-id}", "svid455556", "{service-id56}", "222", "{e-id56}", "asas")]
        [InlineData("http://hackathon.com/{sdgdsg}/service=/{service-id}/{service-id56}", "http://hackathon.com/test123/service=/svid455556/222", "{sdgdsg}", "test123"," {service-id}", "svid455556", "{service-id56}", "222")]
        public void TestMultiLink()
        {

        }
    }
}