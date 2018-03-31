using System;
using System.Collections.Generic;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Helpers;

namespace Interrogator.Mappings
{
    public class RobotMapping
    {
        private RobotMapping(IList<Robot> robots) : this(robots[0], robots[1], robots[2]) { }

        public RobotMapping(Robot robotOne, Robot robotTwo, Robot robotThree)
        {
            if (robotOne == robotTwo || robotOne == robotThree || robotTwo == robotThree)
            {
                throw new InvalidOperationException("All robots should be present exactly once.");
            }

            Mapping = new Dictionary<Position, Robot>
            {
                {Position.One, robotOne},
                {Position.Two, robotTwo},
                {Position.Three, robotThree}
            };
        }

        private IDictionary<Position, Robot> Mapping { get; }

        public Robot this[Position position] => Mapping[position];

        public static IEnumerable<RobotMapping> AllRobotMappings => LazyAllRobotMappings.Value;

        public override string ToString()
        {
            return string.Join(" ", Mapping.Select(kvp => kvp.Value.ToString()));
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (!(obj is RobotMapping otherRobotMapping)) return false;
            return Mapping.Equals(otherRobotMapping.Mapping);
        }

        public override int GetHashCode()
        {
            return Mapping.GetHashCode();
        }

        private static readonly Lazy<IEnumerable<RobotMapping>> LazyAllRobotMappings =
            new Lazy<IEnumerable<RobotMapping>>(() =>
                RobotHelpers.AllRobots().ToList().Permutations().Select(robots => new RobotMapping(robots)));
    }
}
