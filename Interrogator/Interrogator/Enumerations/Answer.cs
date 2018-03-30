using System;
using System.Collections.Generic;
using System.Linq;

namespace Interrogator.Enumerations
{
    public enum Answer
    {
        Yes,
        No
    }

    public static class AnswerHelpers
    {
        public static Answer ToAnswer(this bool b)
        {
            return b ? Answer.Yes : Answer.No;
        }

        public static IReadOnlyList<Answer> AllAnswers()
        {
            return Enum.GetValues(typeof(Answer)).Cast<Answer>().ToList();
        }
    }
}
