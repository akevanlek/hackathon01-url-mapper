using System.Collections.Generic;

namespace UrlMapper
{
    public class SimpleStringParameterBuilder : ISimpleStringParameterBuilder
    {
        public ISimpleStringParameter Parse(string pattern)
        {
            var stringParams = new SimpleStringParameter(pattern);
            return stringParams;
        }
    }
}