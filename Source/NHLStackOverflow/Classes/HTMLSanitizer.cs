using System.Text.RegularExpressions;

namespace NHLStackOverflow.Classes
{
    class HTMLSanitizer
    {
        private static Regex rAnyTag = new Regex("(<)[^>]*(>)", RegexOptions.Multiline);
        private MatchEvaluator evaluator;

        public HTMLSanitizer()
        {
            this.evaluator = new MatchEvaluator(eval);
        }

        private string eval(Match m)
        {
            return m.Value
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");
        }

        public string SanitizeHTMLTags(string text)
        {
            return rAnyTag.Replace(text, evaluator);
        }

        public string StripHTMLTags(string text)
        {
            return rAnyTag.Replace(text, string.Empty);
        }
    }
}