using System;
using System.Collections.Generic;

namespace Task2_TextEditor
{
    public interface IPunctuationContainer : ICloneable
    {
        IEnumerable<string> SentencesSeparators { get; }
        IEnumerable<string> SyntacticConstructionsSeparators { get; }
    }
}
