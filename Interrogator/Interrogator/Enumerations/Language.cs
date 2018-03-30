using System;
using System.Collections.Generic;
using System.Linq;

namespace Interrogator.Enumerations
{
    public enum Language
    {
        Ozo,
        Uzu
    }

    public static class LanguageHelpers
    {
        public static IReadOnlyList<Language> AllWords()
        {
            return Enum.GetValues(typeof(Language)).Cast<Language>().ToList();
        }
    }
}
