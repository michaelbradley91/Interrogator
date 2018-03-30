using System;
using Interrogator.Enumerations;

namespace Interrogator.Mappings
{
    public class AnswerMapping
    {
        public Answer Yes { get; }
        public Answer No { get; }

        private readonly Random _random;

        public AnswerMapping(Answer yesAnswer, Answer noAnswer)
        {
            if (yesAnswer == noAnswer) throw new InvalidOperationException("Yes and no must be distinct.");

            Yes = yesAnswer;
            No = noAnswer;

            _random = new Random();
        }

        public Answer this[bool answer] => answer ? Yes : No;
        public Answer Random() => _random.Next(0, 2) >= 1 ? Yes : No;
    }
}
