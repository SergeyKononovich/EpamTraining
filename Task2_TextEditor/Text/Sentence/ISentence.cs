using System.Collections.Generic;

namespace Task2_TextEditor
{
    public interface ISentence
    {
        ICollection<ISentenceItem> Items { get; }
    }
}
