using System.Collections.Generic;

namespace UrlMapper {
    public class SimpleStringParameterBuilder : ISimpleStringParameterBuilder {
        public ISimpleStringParameter Parse (string pattern) => new SimpleStringParameter (pattern);
    }
}