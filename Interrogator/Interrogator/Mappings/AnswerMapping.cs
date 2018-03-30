using System;
using Interrogator.Enumerations;

namespace Interrogator.Mappings
{
    public class AnswerMapping
    {
        public Language Yes { get; }
        public Language No { get; }

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
    }
}
