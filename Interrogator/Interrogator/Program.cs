using System;
using System.Collections.Generic;
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
            // How to deduce... For each question, get the answer for each mapping, and group mappings based on the answer.
            // For each answer, you can then see where each robot could be. Practice first...
            Console.WriteLine("Starting...");

            foreach (var firstQuestion in GetAllFirstQuestions())
            {
                Console.WriteLine(firstQuestion.Text);
                var firstQuestionResults = GetQuestionResults(firstQuestion);
                AnalyseResults(firstQuestionResults);

                foreach (var firstQuestionResult in firstQuestionResults)
                {
                    foreach (var secondQuestion in GetAllSecondQuestions())
                    {
                        var secondQuestionResults = GetQuestionResults(secondQuestion, firstQuestionResult.Value);
                        AnalyseResults(secondQuestionResults);

                        foreach (var secondQuestionResult in secondQuestionResults)
                        {
                            foreach (var thirdQuestion in GetAllThirdQuestions())
                            {
                                var thirdQuestionResults = GetQuestionResults(thirdQuestion, secondQuestionResult.Value);
                                AnalyseResults(secondQuestionResults);
                            }
                        }
                    }
                }
            }
            // Deduce by looking at all combinations of robots that can produce each answer.
            
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static IEnumerable<IQuestion> GetAllFirstQuestions()
        {
            //return QuestionHelpers.GetAllQuestions(2);
            return new[]
            {
                new IsAnswerQuestion(Position.One, new IsItQuestion(Position.One, Position.Two, Robot.R), Language.Ozo)
            };
        }

        private static IEnumerable<IQuestion> GetAllSecondQuestions()
        {
            //return QuestionHelpers.GetAllQuestions(2);
            return new[]
            {
                new IsAnswerQuestion(Position.Two, new IsItQuestion(Position.Two, Position.One, Robot.R), Language.Ozo)
            };
        }

        private static IEnumerable<IQuestion> GetAllThirdQuestions()
        {
            //return QuestionHelpers.GetAllQuestions(2);
            return new[]
            {
                new IsAnswerQuestion(Position.Two, new IsItQuestion(Position.Two, Position.Two, Robot.R), Language.Ozo)
            };
        }

        private static IDictionary<Language, IList<ProblemMapping>> GetQuestionResults(IQuestion question, IEnumerable<ProblemMapping> problemMappings = null)
        {
            problemMappings = problemMappings ?? ProblemMapping.AllProblemMappings;

            var wordToMapping = LanguageHelpers.AllWords()
                .ToDictionary<Language, Language, IList<ProblemMapping>>(word => word, word => new List<ProblemMapping>());

            foreach (var problemMapping in problemMappings)
            {
                var answers = question.GetPossibleAnswers(problemMapping).OrderBy(a => a);
                foreach (var answer in answers)
                {
                    wordToMapping[problemMapping.AnswerMapping[answer]].Add(problemMapping);
                }
            }

            return wordToMapping;
        }

        private static void AnalyseResults(IDictionary<Language, IList<ProblemMapping>> results)
        {
            foreach (var result in results)
            {
                Console.WriteLine("For answer: " + result.Key + " robot mappings are");
                foreach (var problemMapping in result.Value)
                {
                    Console.WriteLine(problemMapping.RobotMapping + " | " + problemMapping.AnswerMapping);
                }
            }
        }
    }
}
