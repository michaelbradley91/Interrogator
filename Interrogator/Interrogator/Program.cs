using System;
using Interrogator.Enumerations;
using Interrogator.Mappings;
using Interrogator.Questions;

namespace Interrogator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mapping = new ProblemMapping(
                new RobotMapping(Robot.T, Robot.F, Robot.R),
                new AnswerMapping(Language.Ozo, Language.Uzu));

            var question = new IsAnswerQuestion(Position.One, new AreYouQuestion(Position.Two, Robot.F), Language.Ozo);

            var answers = question.GetPossibleAnswers(mapping);

            Console.WriteLine(string.Join(" or ", answers));
            Console.ReadLine();
        }
    }
}
