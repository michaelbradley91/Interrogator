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
                    return mapping.AnswerMapping[Robot == Robot.T];
                case Robot.F:
                    return mapping.AnswerMapping[Robot != Robot.F];
                case Robot.R:
                    return mapping.AnswerMapping.Random();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
