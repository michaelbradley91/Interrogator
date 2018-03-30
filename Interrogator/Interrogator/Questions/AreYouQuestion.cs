using System;
using Interrogator.Enumerations;
using Interrogator.Mappings;

namespace Interrogator.Questions
{
    public class AreYouQuestion : IQuestion
    {
        public Position AddressedTo { get; }
        public Robot Robot { get; }

        public AreYouQuestion(Position position, Robot robot)
        {
            AddressedTo = position;
            Robot = robot;
        }

        public Answer GetAnswer(ProblemMapping mapping)
        {
            switch (mapping.RobotMapping[AddressedTo])
            {
                case Robot.T:
                    return (Robot == Robot.T).ToAnswer();
                case Robot.F:
                    return (Robot != Robot.F).ToAnswer();
                case Robot.R:
                    return Answer.Unknown;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
