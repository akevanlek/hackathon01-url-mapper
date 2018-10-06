using System.Collections.Generic;

namespace UrlMapper {
    public class SimpleStringParameter : ISimpleStringParameter {
        private string Patterns;

        public SimpleStringParameter (string patterns) {
            Patterns = patterns;
        }

        public void ExtractVariables (string target, IDictionary<string, string> dicToStoreResults) {

            if (IsMatched (target)) {
                const int initIndex = 0;
                var indexOfData = Patterns.IndexOf ("{");
                var key = Patterns.Substring (indexOfData);
                var patternUrlForTarget = Patterns.Substring (initIndex, (Patterns.Length - key.Length));
                
                var result = target.Substring (initIndex + patternUrlForTarget.Length);
                dicToStoreResults.Add (key, result);
            }
        }

        public bool IsMatched (string textToCompare) {

            const int initIndex = 0;
            var indexOfData = Patterns.IndexOf ("{");
            var data = Patterns.Substring (indexOfData);

            var patternFormat = Patterns.Substring (initIndex, data.Length);
            return textToCompare.StartsWith (patternFormat);
        }
    }
}