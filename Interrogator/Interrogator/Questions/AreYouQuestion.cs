using System;
using System.Collections.Generic;
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

        public IEnumerable<Answer> GetPossibleAnswers(ProblemMapping mapping)
        {
            switch (mapping.RobotMapping[AddressedTo])
            {
                case Robot.T:
                    return new [] { (Robot == Robot.T).ToAnswer() };
                case Robot.F:
                    return new [] { (Robot != Robot.F).ToAnswer() };
                case Robot.R:
                    return AnswerHelpers.AllAnswers();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
