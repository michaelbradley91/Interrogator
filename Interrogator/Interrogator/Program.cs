using System;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Mappings;
using Interrogator.Questions;

namespace Interrogator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            foreach (var mapping in ProblemMapping.GetAllProblemMappings())
            {
                var question = new IsAnswerQuestion(Position.One, new IsItQuestion(Position.Two, Position.Two, Robot.F), Language.Uzu);
                var answers = question.GetPossibleAnswers(mapping).OrderBy(a => a);

                Console.WriteLine(string.Join(" or ", answers).PadRight(9) + " | " + mapping);
            }

            Console.ReadLine();
        }
    }
}
