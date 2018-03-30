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
            foreach (var mapping in ProblemMapping.AllProblemMappings)
            {
                foreach (var question in QuestionHelpers.GetAllQuestions(1))
                {
                    var answers = question.GetPossibleAnswers(mapping).OrderBy(a => a);

                    Console.WriteLine(string.Join(" or ", answers).PadRight(9) + " | " + mapping + " | " + question.Text);
                }                
            }

            Console.ReadLine();
        }
    }
}
