using System;
using System.Collections.Generic;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Mappings;

namespace Interrogator.Questions
{
    public class IsItQuestion : IQuestion, ISimpleQuestion
    {
        public Position AddressedTo { get; }
        public Position Target { get; }
        public Robot Robot { get; }

        public IsItQuestion(Position position, Position target, Robot robot)
        {
            AddressedTo = position;
            Target = target;
            Robot = robot;
        }

        public IEnumerable<Answer> GetPossibleAnswers(ProblemMapping mapping)
        {
            switch (mapping.RobotMapping[AddressedTo])
            {
                case Robot.T:
                    return new [] { (mapping.RobotMapping[Target] == Robot.T).ToAnswer() };
                case Robot.F:
                    return new [] { (mapping.RobotMapping[Target] != Robot.F).ToAnswer() };
                case Robot.R:
                    return AnswerHelpers.AllAnswers();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        IEnumerable<IQuestion> ISimpleQuestion.GetAllPossibleQuestions()
        {
            return GetAllPossibleQuestions();
        }

        public static IEnumerable<IQuestion> GetAllPossibleQuestions()
        {
            var positions = PositionHelpers.AllPositions();
            var robots = RobotHelpers.AllRobots();
            return 
                from robot in robots
                from position in positions
                from target in positions
                select new IsItQuestion(position, target, robot);
        }

        public string Text => "Asking robot " + AddressedTo + " is robot " + Target + " " + Robot;
    }
}
