using System;
using System.Collections.Generic;
using System.Linq;

namespace Interrogator.Enumerations
{
    public enum Position
    {
        One,
        Two,
        Three
    }

    public static class PositionHelpers
    {
        public static IReadOnlyList<Position> AllPositions()
        {
            return Enum.GetValues(typeof(Position)).Cast<Position>().ToList();
        }
    }
}
