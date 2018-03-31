using System;
using System.Collections.Generic;
using System.Linq;

namespace Interrogator.Enumerations
{
    public enum Word
    {
        Ozo,
        Uzu
    }

    public static class WordHelpers
    {
        public static IReadOnlyList<Word> AllWords()
        {
            return Enum.GetValues(typeof(Word)).Cast<Word>().ToList();
        }
    }
}
