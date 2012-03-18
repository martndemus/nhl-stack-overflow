using System.Text.RegularExpressions;

namespace NHLStackOverflow.Classes
{
    /// <summary>
    /// An enum with options available for the HTMLSanitizer
    /// </summary>
    public enum HTMLSanitizerOption
    {
        /// <summary>
        /// Will strip all HTML like tags from the input text
        /// </summary>
        StripTags

        /// <summary>
        /// Unescapes Markdown language characters.
        /// </summary>
      , UnescapeMarkDown
    }

    /// <summary>
    /// A simple class that does the basic HTML sanitizing.
    /// 
    /// You can supply 2 additional options to the sanitize method.
    /// - StripTags: the sanitizer will strip out all HTML like tags.
    /// - UnescapeMarkDown: will unescape a few of the common by markdown used entities. 
    /// </summary>
    class HTMLSanitizer
    {
        private static Regex rAnyTag = new Regex("(<)[^>]*(>)", RegexOptions.Multiline);
        private static Regex rMDBlockQuotes = new Regex("^(&gt;( )?)+", RegexOptions.Multiline);

        /// <summary>
        /// Sanitizes the input string from HTML.
        /// </summary>
        /// <param name="text">Input string</param>
        /// <param name="options">Additional options</param>
        /// <returns>Sanitized string</returns>
        public string Sanitize(string text, params HTMLSanitizerOption[] options)
        {
            // Some simple boolean flag for the options
            bool striptags = false
               , unescapeMD = false;

            // Check all options
            foreach (HTMLSanitizerOption h in options)
            {
                if (h == HTMLSanitizerOption.StripTags)
                    striptags = true;
                if (h == HTMLSanitizerOption.UnescapeMarkDown)
                    unescapeMD = true;
            }

            // Unify line endings
            text = text.Replace("\r\n", "\n").Replace("\r", "\n");

            // If the strip tags option is given, strip all tags out.
            if (striptags == true)
                text = rAnyTag.Replace(text, "");

            // Escape the common HTML entities. 
            text = EscapeHTMLentities(text);

            // Unescape the markdown blockquote entity.
            if (unescapeMD == true)
                text = rMDBlockQuotes.Replace(text, new MatchEvaluator(unescapeMDBlockQuote));

            return text;
        }

        /// <summary>
        /// Escapes &, <, >, ", '
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>Escaped text</returns>
        public string EscapeHTMLentities(string text)
        {
            return text
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }

        private string unescapeMDBlockQuote(Match m)
        {
            return m.Value.Replace("&gt;", ">");
        }
    }
}