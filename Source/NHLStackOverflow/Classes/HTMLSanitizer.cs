using System.Collections;
using System.Collections.Generic;
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
    public class HTMLSanitizer
    {
        private static Regex rMDEqualsHR = new Regex("^[&#61;( )?]+", RegexOptions.Multiline);
        private static Regex rMDBlockQuotes = new Regex("^(&gt;( )?)+", RegexOptions.Multiline);

        private static string HTMLTagEntities = "</>";
        private static readonly Hashtable HTMLTags;


        static HTMLSanitizer()
        {
            HTMLTags = new Hashtable();

            // ADD ALL THE TAGS TO ZE HAZTABLE.
            HTMLTags.Add("html", null);
            HTMLTags.Add("head", null);
            HTMLTags.Add("title", null);
            HTMLTags.Add("base", null);
            HTMLTags.Add("link", null);
            HTMLTags.Add("meta", null);
            HTMLTags.Add("style", null);
            HTMLTags.Add("script", null);
            HTMLTags.Add("noscript", null);
            HTMLTags.Add("body", null);
            HTMLTags.Add("section", null);
            HTMLTags.Add("nav", null);
            HTMLTags.Add("article", null);
            HTMLTags.Add("aside", null);
            HTMLTags.Add("h1", null);
            HTMLTags.Add("h2", null);
            HTMLTags.Add("h3", null);
            HTMLTags.Add("h4", null);
            HTMLTags.Add("h5", null);
            HTMLTags.Add("h6", null);
            HTMLTags.Add("hgroup", null);
            HTMLTags.Add("header", null);
            HTMLTags.Add("footer", null);
            HTMLTags.Add("address", null);
            HTMLTags.Add("p", null);
            HTMLTags.Add("hr", null);
            HTMLTags.Add("pre", null);
            HTMLTags.Add("blockquote", null);
            HTMLTags.Add("ol", null);
            HTMLTags.Add("ul", null);
            HTMLTags.Add("li", null);
            HTMLTags.Add("dl", null);
            HTMLTags.Add("dt", null);
            HTMLTags.Add("dd", null);
            HTMLTags.Add("figure", null);
            HTMLTags.Add("figcaption", null);
            HTMLTags.Add("div", null);
            HTMLTags.Add("a", null);
            HTMLTags.Add("em", null);
            HTMLTags.Add("strong", null);
            HTMLTags.Add("small", null);
            HTMLTags.Add("s", null);
            HTMLTags.Add("cite", null);
            HTMLTags.Add("q", null);
            HTMLTags.Add("dfn", null);
            HTMLTags.Add("abbr", null);
            HTMLTags.Add("time", null);
            HTMLTags.Add("code", null);
            HTMLTags.Add("var", null);
            HTMLTags.Add("samp", null);
            HTMLTags.Add("kbd", null);
            HTMLTags.Add("sub", null);
            HTMLTags.Add("sup", null);
            HTMLTags.Add("i", null);
            HTMLTags.Add("b", null);
            HTMLTags.Add("u", null);
            HTMLTags.Add("mark", null);
            HTMLTags.Add("ruby", null);
            HTMLTags.Add("rt", null);
            HTMLTags.Add("rp", null);
            HTMLTags.Add("bdi", null);
            HTMLTags.Add("bdo", null);
            HTMLTags.Add("span", null);
            HTMLTags.Add("br", null);
            HTMLTags.Add("wbr", null);
            HTMLTags.Add("ins", null);
            HTMLTags.Add("del", null);
            HTMLTags.Add("img", null);
            HTMLTags.Add("iframe", null);
            HTMLTags.Add("embed", null);
            HTMLTags.Add("object", null);
            HTMLTags.Add("param", null);
            HTMLTags.Add("video", null);
            HTMLTags.Add("audio", null);
            HTMLTags.Add("source", null);
            HTMLTags.Add("track", null);
            HTMLTags.Add("canvas", null);
            HTMLTags.Add("map", null);
            HTMLTags.Add("area", null);
            HTMLTags.Add("table", null);
            HTMLTags.Add("caption", null);
            HTMLTags.Add("colgroup", null);
            HTMLTags.Add("col", null);
            HTMLTags.Add("tbody", null);
            HTMLTags.Add("thead", null);
            HTMLTags.Add("tfoot", null);
            HTMLTags.Add("tr", null);
            HTMLTags.Add("td", null);
            HTMLTags.Add("th", null);
            HTMLTags.Add("form", null);
            HTMLTags.Add("fieldset", null);
            HTMLTags.Add("legend", null);
            HTMLTags.Add("label", null);
            HTMLTags.Add("input", null);
            HTMLTags.Add("button", null);
            HTMLTags.Add("select", null);
            HTMLTags.Add("datalist", null);
            HTMLTags.Add("optgroup", null);
            HTMLTags.Add("option", null);
            HTMLTags.Add("textarea", null);
            HTMLTags.Add("keygen", null);
            HTMLTags.Add("output", null);
            HTMLTags.Add("progress", null);
            HTMLTags.Add("meter", null);
            HTMLTags.Add("details", null);
            HTMLTags.Add("summary", null);
            HTMLTags.Add("command", null);
            HTMLTags.Add("menu", null);
        }

        /// <summary>
        /// Whether the given char is in the range of a-zA-z0-9.
        /// </summary>
        /// <param name="c">A character</param>
        private static bool isAlphaNum(char c)
        {
            return c >= 97 && c <= 122 || c >= 65 && c <= 90 || c >= 48 && c <= 57;
        }

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
                text = StripTags(text);
            //text = rAnyTag.Replace(text, "");

            // Escape the common HTML entities. 
            text = EscapeHTMLentities(text);

            // Unescape the markdown blockquote entity.
            if (unescapeMD == true)
            {
                text = rMDBlockQuotes.Replace(text, new MatchEvaluator(unescapeGreaterThen));
                text = rMDEqualsHR.Replace(text, new MatchEvaluator(unescapeEquals));
            }

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
                .Replace("'", "&#x27;");
        }

        /// <summary>
        /// Escapes any ASCII character that is not alphanumeric with HTML encoding.
        /// </summary>
        /// <param name="text">Input string</param>
        /// <returns>Escaped string</returns>
        public string EscapeHTMLStrict(string text)
        {
            return EscapeASCII(text, "&#x{0};");
        }

        /// <summary>
        /// Escapes any ASCII character that is not alphanumeric with URL encoding.
        /// </summary>
        /// <param name="text">Input string</param>
        /// <returns>Escaped string</returns>
        public string UrlEncode(string text)
        {
            return EscapeASCII(text, "%{0}");
        }

        /// <summary>
        /// Strips out all <strong>known</strong> HTML tags including the content inbetween it.
        /// <remarks>UNKNOWN TAGS WILL NOT BE STRIPPED</remarks>
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>Text stripped of all known HTML tags</returns>
        public string StripTags(string text)
        {
            Stack<int> opentags = new Stack<int>();
            int start;
            string temp;

            // Iterate through the input string and search for < > characters
            for (int i = 0; i < text.Length; i++)
            {
                // Push opening brackets onto the stack.
                if (text[i] == '<')
                    opentags.Push(i);

                // End > is found, go check if it needs to be removed.
                if (text[i] == '>' && opentags.Count > 0)
                {
                    start = opentags.Pop();

                    // Get the tagname
                    string tag = "";
                    for (int k = 0; start + k < text.Length && (isAlphaNum(text[start + k]) || HTMLTagEntities.IndexOf(text[start + k]) >= 0); k++)
                    {
                        if (text[start + k] == '<' || text[start + k] == '/') continue;
                        if (text[start + k] == '>') break;

                        tag += text[start + k];
                    }

                    // Strip if it's a known tag.
                    if (HTMLTags.ContainsKey(tag.ToLower()))
                    {
                        // Strip out the tag
                        temp = text.Substring(0, start);
                        temp += text.Substring(i + 1);
                        text = temp;

                        // Go back to where the opening '<' was.
                        i = start;
                    }
                }
            }

            return text;
        }

        private string EscapeASCII(string text, string format)
        {
            string s = string.Empty;

            for (int i = 0; i < text.Length; i++)
                if (text[i] < 256 && !isAlphaNum(text[i]))
                    s += string.Format(format, (int)text[i]);
                else
                    s += text[i];

            return s;
        }

        private string unescapeGreaterThen(Match m)
        {
            return m.Value.Replace("&gt;", ">");
        }

        private string unescapeEquals(Match m)
        {
            return m.Value.Replace("&#61;", "=");
        }
    }
}