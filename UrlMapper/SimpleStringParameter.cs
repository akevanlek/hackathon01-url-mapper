using System.Collections.Generic;

namespace UrlMapper {
    public class SimpleStringParameter : ISimpleStringParameter {
        private string Patterns;

        public SimpleStringParameter (string patterns) {
            Patterns = patterns;
        }

        public void ExtractVariables (string target, IDictionary<string, string> dicToStoreResults) {

            var linkto_key = "https://mana.com/linkto/";
            var isMatchWithLinkToUrl = target.StartsWith (linkto_key);

            if (isMatchWithLinkToUrl) {

                var index = target.IndexOf (linkto_key);
                var result = target.Substring (index + linkto_key.Length);
                dicToStoreResults.Add ("{link-id}", result);
            }

        }

        public bool IsMatched (string textToCompare) {

            var indexOfData = Patterns.IndexOf ("/{");
            var xxx = Patterns.Substring (indexOfData);

            var x = Patterns.Substring (0, xxx.Length);
            return textToCompare.StartsWith(x);
        }
    }
}