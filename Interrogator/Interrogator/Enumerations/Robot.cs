using System;
using System.Collections.Generic;
using System.Linq;

namespace Interrogator.Enumerations
{
    public enum Robot
    {
        T,
        F,
        R
    }

    public static class RobotHelpers
    {
        public static IReadOnlyList<Robot> AllRobots()
        {
            return Enum.GetValues(typeof(Robot)).Cast<Robot>().ToList();
        }
    }
}
