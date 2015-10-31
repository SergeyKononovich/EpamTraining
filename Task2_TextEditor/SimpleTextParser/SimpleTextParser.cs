using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task2_TextEditor
{
    public class SimpleTextParser
    {
        private static readonly Regex TrimSpacesRegex = new Regex(@"\s+");
        private const int Bufferlength = 10000;
        private readonly Regex _textParserRegex;
        private readonly Regex _sentenceParserRegex;
        private readonly IPunctuationContainer _punctuationContainer;

        public IPunctuationContainer PunctuationContainer
        {
            get { return _punctuationContainer.Clone() as IPunctuationContainer; }
        }


        public SimpleTextParser(IPunctuationContainer punctuationContainer)
        {
            if (punctuationContainer == null) throw new ArgumentNullException(nameof(punctuationContainer));
            var punctConCopy = punctuationContainer.Clone() as IPunctuationContainer;
            if (IsPunctuationInvalid(punctConCopy))
                throw new FormatException("Punctuation must not contain spaces or be empty");

            _punctuationContainer = punctConCopy;

            // set _textParserRegex
            var orderedSep = PunctuationContainer.SentencesSeparators.OrderByDescending(x => x.Length);
            string sep1 = String.Join(@"|", orderedSep.Select(Regex.Escape).ToArray());
            string pattern = $"(.+?({sep1}))(?=(\\s+|$))";
            _textParserRegex = new Regex(pattern);

            // set _sentenceParserRegex
            string sep2 = String.Join(@"", orderedSep.Select(Regex.Escape).ToArray());
            orderedSep = PunctuationContainer.SyntacticConstructionsSeparators.OrderByDescending(x => x.Length);
            string sep3 = String.Join(@"|", orderedSep.Select(Regex.Escape).ToArray());
            string sep4 = String.Join(@"", orderedSep.Select(Regex.Escape).ToArray());
            pattern = $"([^{sep4}\\s{sep2}]+|{sep1}|\\s|{sep3})";
            _sentenceParserRegex = new Regex(pattern);
        }

        public virtual Text Parse(TextReader reader)
        {
            Text textResult = new Text();
            char[] buffer = new char[Bufferlength];
            StringBuilder builder = new StringBuilder(Bufferlength);

            int readCharsCount = 0;
            do
            {
                readCharsCount = reader.ReadBlock(buffer, 0, Bufferlength);
                if (readCharsCount <= 0) continue;

                builder.Append(buffer, 0, readCharsCount);
                int endIndex;
                textResult += ParseText(builder.ToString(), out endIndex);
                builder.Remove(0, endIndex + 1);
            } while (readCharsCount != 0);

            return textResult;
        }

        protected virtual Text ParseText(string text, out int endIndex)
        {
            text = TrimSpaces(text);
            Text result = new Text();
            
            var matches = _textParserRegex.Matches(text);
            foreach (Match match in matches)
            {
                string sentence = match.Value.Trim();
                if (sentence != String.Empty)
                    result.Add(ParseTextItem(sentence));
            }

            if (matches.Count != 0)
            {
                Match lastMatch = matches[matches.Count - 1];
                endIndex = lastMatch.Index + lastMatch.Length - 1;
            }
            else
                endIndex = 0;

            return result;
        }
        protected virtual ITextItem ParseTextItem(string sentence)
        {
            var result = new Sentence();

            var matches = _sentenceParserRegex.Matches(sentence);
            foreach (Match match in matches)
                result.Add(GetSentenceItemFromString(match.Value));

            return result;
        }
        protected virtual ISentenceItem GetSentenceItemFromString(string str)
        {
            var common = PunctuationContainer.SyntacticConstructionsSeparators
                .Intersect(PunctuationContainer.SentencesSeparators);

            if (str == " ")
                return new Space();
            else if (common.Contains(str))
                return new Punctuation(str, true, true);
            else if (PunctuationContainer.SyntacticConstructionsSeparators.Contains(str))
                return new Punctuation(str, false, true);
            else if (PunctuationContainer.SentencesSeparators.Contains(str))
                return new Punctuation(str, true);
            else
                return new Word(str);
        }

        static private bool IsPunctuationInvalid(IPunctuationContainer punctuation)
        {
            return punctuation.SentencesSeparators.Any(sep => sep == String.Empty || sep.Contains(" ")) ||
                   punctuation.SyntacticConstructionsSeparators.Any(sep => sep == String.Empty || sep.Contains(" "));
        }
        static private string TrimSpaces(string text)
        {
            return TrimSpacesRegex.Replace(text, " ");
        }
    }
}
