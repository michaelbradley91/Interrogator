using System;
using System.Collections;
using System.Collections.Generic;
using Interrogator.Enumerations;

namespace Interrogator.Mappings
{
    public class RobotMapping : IEnumerable<KeyValuePair<Position, Robot>>
    {
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

        public IEnumerator<KeyValuePair<Position, Robot>> GetEnumerator()
        {
            return Mapping.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
