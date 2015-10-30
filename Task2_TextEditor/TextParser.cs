using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task2_TextEditor
{
    public class TextParser
    {
        private static readonly Regex TrimSpacesRegex = new Regex(@"\s+");
        private const int Bufferlength = 10000;
        private readonly Regex _textParserRegex;
        private readonly Regex _sentenceParserRegex;

        public TextSeparatorContainer TextSeparatorContainer { get; }

        
        public TextParser(TextSeparatorContainer textSeparatorContainer)
        {
            if (textSeparatorContainer == null) throw new ArgumentNullException(nameof(textSeparatorContainer));

            TextSeparatorContainer = textSeparatorContainer;

            // set _textParserRegex
            var orderedSep = TextSeparatorContainer.SentencesSeparators.OrderByDescending(x => x.Length);
            string sep1 = String.Join(@"|", orderedSep.Select(Regex.Escape).ToArray());
            string pattern = $"(.+?({sep1}))(?=(\\s+|$))";
            _textParserRegex = new Regex(pattern);

            // set _sentenceParserRegex
            string sep2 = String.Join(@"", orderedSep.Select(Regex.Escape).ToArray());
            orderedSep = TextSeparatorContainer.SyntacticConstructionsSeparators.OrderByDescending(x => x.Length);
            string sep3 = String.Join(@"|", orderedSep.Select(Regex.Escape).ToArray());
            string sep4 = String.Join(@"", orderedSep.Select(Regex.Escape).ToArray());
            pattern = $"([^{sep4}\\s{sep2}]+|{sep1}|\\s|{sep3})";
            _sentenceParserRegex = new Regex(pattern);
        }

        public Text Parse(TextReader reader)
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
        protected Text ParseText(string text, out int endIndex)
        {
            text = TrimSpaces(text);
            Text result = new Text();
            
            var matches = _textParserRegex.Matches(text);
            foreach (Match match in matches)
            {
                string sentence = match.Value.Trim();
                if (sentence != String.Empty)
                    result.Sentences.Add(ParseSentence(sentence));
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
        protected ISentence ParseSentence(string sentence)
        {
            ISentence result = new Sentence();

            var matches = _sentenceParserRegex.Matches(sentence);
            foreach (Match match in matches)
                result.Items.Add(GetSentenceItemFromString(match.Value));

            return result;
        }

        protected ISentenceItem GetSentenceItemFromString(string str)
        {
            // get common separators
            var common = TextSeparatorContainer.SyntacticConstructionsSeparators
                .Join(TextSeparatorContainer.SentencesSeparators, x => x, y => y, (x, y) => x);

            if (str == " ")
                return new Space();
            else if (common.Contains(str))
                return new Punctuation(str, true, true);
            else if (TextSeparatorContainer.SyntacticConstructionsSeparators.Contains(str))
                return new Punctuation(str, false, true);
            else if (TextSeparatorContainer.SentencesSeparators.Contains(str))
                return new Punctuation(str, true, false);
            else
                return new Word(str);
        }
        static private string TrimSpaces(string text)
        {
            return TrimSpacesRegex.Replace(text, " ");
        }
    }
}
