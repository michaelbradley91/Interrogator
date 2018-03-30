using System;
using System.Collections.Generic;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Helpers;

namespace Interrogator.Mappings
{
    public class AnswerMapping
    {
        public Language Yes { get; }
        public Language No { get; }

        private AnswerMapping(IList<Language> words) : this(words[0], words[1]) { }

        public AnswerMapping(Language yesAnswer, Language noAnswer)
        {
            if (yesAnswer == noAnswer) throw new InvalidOperationException("Yes and no must be distinct.");

            Yes = yesAnswer;
            No = noAnswer;
        }

        public Language this[Answer answer]
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

        public static IEnumerable<AnswerMapping> GetAllAnswerMappings()
        {
            return LanguageHelpers.AllWords().ToList().Permutations().Select(language => new AnswerMapping(language));
        }

        public override string ToString()
        {
            return Answer.Yes + "=" + Yes + " " + Answer.No + "=" + No;
        }
    }
}
