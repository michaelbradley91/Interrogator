using System;
using System.Collections.Generic;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Helpers;

namespace Interrogator.Mappings
{
    public class AnswerMapping
    {
        public Word Yes { get; }
        public Word No { get; }

        private AnswerMapping(IList<Word> words) : this(words[0], words[1]) { }

        public AnswerMapping(Word yesAnswer, Word noAnswer)
        {
            if (yesAnswer == noAnswer) throw new InvalidOperationException("Yes and no must be distinct.");

            Yes = yesAnswer;
            No = noAnswer;
        }

        public Word this[Answer answer]
        {
            get
            {
                switch (answer)
                {
                    case Answer.Yes:
                        return Yes;
                    case Answer.No:
                        return No;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(answer), answer, null);
                }
            }
        }

        public static IEnumerable<AnswerMapping> AllAnswerMappings => LazyAllAnswerMappings.Value;

        public override string ToString()
        {
            return Answer.Yes + "=" + Yes + " " + Answer.No + "=" + No;
        }

        private static readonly Lazy<IEnumerable<AnswerMapping>> LazyAllAnswerMappings =
            new Lazy<IEnumerable<AnswerMapping>>(() =>
                WordHelpers.AllWords().ToList().Permutations().Select(language => new AnswerMapping(language)));
    }
}
