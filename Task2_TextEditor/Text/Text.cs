using System.Collections.Generic;

namespace Task2_TextEditor
{
    public class Text
    {
        public ICollection<ISentence> Sentences { get; set; }

        public Text()
        {
            Sentences = new List<ISentence>();
        }
    }
}
