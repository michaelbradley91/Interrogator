using System;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Helpers;
using Interrogator.Mappings;
using Interrogator.Questions;

namespace Interrogator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var numberOfQuestions = 0;
            Console.WriteLine("Starting...");
            foreach (var mapping in ProblemMapping.AllProblemMappings)
            {
                foreach (var question in QuestionHelpers.GetAllQuestions(1).Concat(QuestionHelpers.GetAllQuestions(2)))
                {
                    var answers = question.GetPossibleAnswers(mapping).OrderBy(a => a);
                }
            }
            Console.WriteLine("Done. Asked this many questions: " + numberOfQuestions);
            // Deduce by looking at all combinations of robots that can produce each answer.
            
            Console.ReadLine();
        }
    }
}
