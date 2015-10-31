using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2_TextEditor
{
    public class PunctuationContainer : IPunctuationContainer
    {
        private readonly IEnumerable<string> _sentencesSeparators;
        private readonly IEnumerable<string> _syntacticConstructionsSeparators;

        public static readonly PunctuationContainer Default;
        public IEnumerable<string> SentencesSeparators => _sentencesSeparators.ToList();
        public IEnumerable<string> SyntacticConstructionsSeparators => _syntacticConstructionsSeparators.ToList();


        static PunctuationContainer()
        {
            var sentencesSeparators = new List<string> { ".", ";", "!", "?", "!?", "?!", "..."};
            var syntacticConstructionsSeparators = new List<string> { ",", "(", ")", "\"", "\'", ":" };
            Default = new PunctuationContainer(sentencesSeparators, syntacticConstructionsSeparators);
        }
        protected PunctuationContainer(PunctuationContainer clone)
        {
            _sentencesSeparators = clone._sentencesSeparators.ToList();
            _syntacticConstructionsSeparators = clone._syntacticConstructionsSeparators.ToList();
        }
        public PunctuationContainer(IEnumerable<string> sentencesSeparators,
            IEnumerable<string> syntacticConstructionsSeparators)
        {
            if (sentencesSeparators == null) throw new ArgumentNullException(nameof(sentencesSeparators));
            if (syntacticConstructionsSeparators == null)
                throw new ArgumentNullException(nameof(syntacticConstructionsSeparators));

            _sentencesSeparators = sentencesSeparators.ToList();
            _syntacticConstructionsSeparators = syntacticConstructionsSeparators.ToList();
        }

        public object Clone()
        {
            return new PunctuationContainer(this);
        }
    }
}
