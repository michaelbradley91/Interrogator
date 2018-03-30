using System;
using Interrogator.Enumerations;

namespace Interrogator.Mappings
{
    public class AnswerMapping
    {
        public Language Yes { get; }
        public Language No { get; }

        private readonly Random _random;

        public AnswerMapping(Language yesAnswer, Language noAnswer)
        {
            if (yesAnswer == noAnswer) throw new InvalidOperationException("Yes and no must be distinct.");

            Yes = yesAnswer;
            No = noAnswer;

            _random = new Random();
        }
    }
}
