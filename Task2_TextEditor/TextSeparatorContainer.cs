using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_TextEditor
{
    public class TextSeparatorContainer
    {
        private readonly IEnumerable<string> _sentencesSeparators;
        private readonly IEnumerable<string> _syntacticConstructionsSeparators;

        public static readonly TextSeparatorContainer Default;
        public IEnumerable<string> SentencesSeparators => _sentencesSeparators.ToList();
        public IEnumerable<string> SyntacticConstructionsSeparators => _syntacticConstructionsSeparators.ToList();


        static TextSeparatorContainer()
        {
            var sentencesSeparators = new List<string> { ".", ";", "!", "?", "!?", "?!", "..."};
            var syntacticConstructionsSeparators = new List<string> { ",", "(", ")", "\"", "\'", ":", " - " };
            Default = new TextSeparatorContainer(sentencesSeparators, syntacticConstructionsSeparators);
        }
        public TextSeparatorContainer(IEnumerable<Symbol> sentencesSeparators,
            IEnumerable<Symbol> syntacticConstructionsSeparators)
        {
            if (sentencesSeparators == null) throw new ArgumentNullException(nameof(sentencesSeparators));
            if (syntacticConstructionsSeparators == null)
                throw new ArgumentNullException(nameof(syntacticConstructionsSeparators));

            _sentencesSeparators = sentencesSeparators.Select(x => x.Chars).AsEnumerable();
            _syntacticConstructionsSeparators = syntacticConstructionsSeparators.Select(x => x.Chars).AsEnumerable();
        }
        public TextSeparatorContainer(IEnumerable<string> sentencesSeparators,
            IEnumerable<string> syntacticConstructionsSeparators)
        {
            if (sentencesSeparators == null) throw new ArgumentNullException(nameof(sentencesSeparators));
            if (syntacticConstructionsSeparators == null)
                throw new ArgumentNullException(nameof(syntacticConstructionsSeparators));

            _sentencesSeparators = sentencesSeparators;
            _syntacticConstructionsSeparators = syntacticConstructionsSeparators;
        }
    }
}
