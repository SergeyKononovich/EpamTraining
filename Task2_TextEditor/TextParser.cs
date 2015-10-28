using System.IO;
using System.Linq;
using System.Text;

namespace Task2_TextEditor
{
    public class TextParser
    {
        private SeparatorContainer _separatorContainer;

        protected SeparatorContainer SeparatorContainer
        {
            get { return _separatorContainer; }
            set { _separatorContainer = value; }
        }


        public TextParser(SeparatorContainer separatorContainer)
        {
            SeparatorContainer = separatorContainer;
        }

        public Text Parse(TextReader reader)
        {
            var orderedSentenceSeparators = SeparatorContainer.SentenceSeparators().OrderByDescending(x => x.Length);
            int bufferlength = 10000;
            Text textResult = new Text();
            StringBuilder buffer = new StringBuilder(bufferlength);

            while (true)
            {
                buffer.Clear();
                string currentString = reader.ReadLine();
                while (currentString != null)
                {
                    int firstSentenceSeparatorOccurence = -1;
                    string firstSentenceSeparator = orderedSentenceSeparators.FirstOrDefault(
                        x =>
                        {
                            firstSentenceSeparatorOccurence = currentString.IndexOf(x);
                            return firstSentenceSeparatorOccurence >= 0;
                        });
                    if (firstSentenceSeparator != null)
                    {
                        buffer.Append(currentString.Substring(0, firstSentenceSeparatorOccurence + firstSentenceSeparator.Length));
                        textResult.Sentences.Add(ParseSentence(buffer.ToString()));
                        buffer.Clear();
                        buffer.Append(currentString.Substring(firstSentenceSeparatorOccurence + firstSentenceSeparator.Length + 1, currentString.Length));
                    }
                    else
                    {
                        buffer.Append(" ");
                        buffer.Append(currentString);
                    }
                    currentString = reader.ReadLine();
                }

            }
        }

        protected ISentence ParseSentence(string source)
        {
        }
    }
}
