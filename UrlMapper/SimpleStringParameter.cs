using System.Collections.Generic;

namespace UrlMapper {
    public class SimpleStringParameter : ISimpleStringParameter {
        private string Patterns;

        private List<string> pattern_format = new List<string> ();
        private List<string> pattern_keys = new List<string> ();

        public SimpleStringParameter (string patterns) {
            Patterns = patterns;
        }

        public void ExtractVariables (string target, IDictionary<string, string> dicToStoreResults) {

            if (IsMatched (target)) {
                for (int index = 0; index < pattern_format.Count; index++) {
                    var result = string.Empty;
                    if (pattern_format.Count > 1) {

                        var startIndex = 0;
                        if (index == startIndex) startIndex = pattern_format[index].Length;
                        else startIndex = target.IndexOf (pattern_format[index]) + pattern_format[index].Length;

                        result = target.Substring (startIndex);
                        if (index + 1 < pattern_format.Count) {

                            var x = result.IndexOf (pattern_format[index + 1]);
                            var backward = result.Substring (x).Length;
                            var substringLength = result.Length - backward;
                            result = target.Substring (startIndex, substringLength);
                        }

                    } else result = target.Substring (pattern_format[index].Length);
                    dicToStoreResults.Add (pattern_keys[index], result);
                }
            }
        }

        public bool IsMatched (string textToCompare) {
            const int StartIndex = 0;
            pattern_format = new List<string> ();
            pattern_keys = new List<string> ();
            var findingUrl = Patterns;
            do {

                var indexOfOpenKey = findingUrl.IndexOf ("{");
                var indexOfCloseKey = findingUrl.IndexOf ("}");
                var key_open = findingUrl.Substring (indexOfOpenKey);
                var key_close = findingUrl.Substring (indexOfCloseKey + 1);
                var key = findingUrl.Substring (indexOfOpenKey, key_open.Length - key_close.Length);
                pattern_keys.Add (key);

                var pattern_startSubString = findingUrl.IndexOf (key);
                findingUrl = findingUrl.Substring (pattern_startSubString + key.Length);

                var isFirstPattern_format = pattern_format.Count < 1;
                var patternFormat = string.Empty;
                if (isFirstPattern_format) patternFormat = Patterns.Substring (StartIndex, (Patterns.Length - (findingUrl.Length + key.Length)));
                else {
                    var previous_key = pattern_keys[pattern_keys.Count - 2];
                    var startSubstring = Patterns.IndexOf (previous_key) + previous_key.Length;
                    var sbustring_backward = Patterns.Substring (startSubstring);
                    patternFormat = sbustring_backward.Substring (StartIndex, (sbustring_backward.Length - pattern_keys[pattern_keys.Count - 1].Length));
                }
                pattern_format.Add (patternFormat);

            }
            while (HasAnyParamerterLeft (findingUrl));

            foreach (var item in pattern_format) {

                if (!textToCompare.Contains (item)) {
                    return false;
                }
            }
            return true;
        }

        private bool HasAnyParamerterLeft (string url) {

            var isValidUrl = !string.IsNullOrWhiteSpace (url);
            if (!isValidUrl) return false;

            const int NextIndex = 1;
            var indexOfKey = Patterns.IndexOf ("}");
            var key = Patterns.Substring (indexOfKey + NextIndex);
            return key.Contains ("{");
        }
    }
}