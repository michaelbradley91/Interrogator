using System;
using System.Collections.Generic;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Mappings;
using Interrogator.Questions;
using Interrogator.Results;

namespace Interrogator
{
    public class Program
    {
        private const int MaxQuestionDepth = 2;
        private const int MaxNumberOfQuestions = 3;

        public static void Main(string[] args)
        {
            // How to deduce... For each question, get the answer for each mapping, and group mappings based on the answer.
            // For each answer, you can then see where each robot could be. Practice first...
            Console.WriteLine("Starting...");

            var questionTree = FindQuestionTree(ProblemMapping.AllProblemMappings.ToList(), 1);
            if (questionTree != null)
            {
                Console.WriteLine(questionTree.GetText());
            }
            // Deduce by looking at all combinations of robots that can produce each answer.

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static QuestionTree FindQuestionTree(IList<ProblemMapping> problemMappings, int questionNumber)
        {
            if (questionNumber == MaxNumberOfQuestions)
            {
                foreach (var question in GetAllQuestions())
                {
                    var allAnswersYieldOneMapping = true;
                    var results = GetQuestionResults(question, problemMappings);
                    foreach (var result in results)
                    {
                        allAnswersYieldOneMapping &= result.Value.Select(p => p.RobotMapping).Distinct().Count() == 1;
                    }

                    if (allAnswersYieldOneMapping)
                    {
                        return new QuestionTree(question);
                    }
                }

                return null;
            }

            var numberOfQuestions = 0;
            foreach (var question in GetAllQuestions())
            {
                if (questionNumber == 1)
                {
                    numberOfQuestions++;
                    Console.WriteLine(numberOfQuestions + ": " + question.Text);
                }
                var questionTree = new QuestionTree(question);
                var allAnswersYieldOneMapping = true;
                var results = GetQuestionResults(question, problemMappings);
                foreach (var result in results)
                {
                    var childQuestionTree = FindQuestionTree(result.Value, questionNumber + 1);
                    if (childQuestionTree == null)
                    {
                        allAnswersYieldOneMapping = false;
                        break;
                    }
                    questionTree.Children.Add(result.Key, childQuestionTree);
                }

                if (allAnswersYieldOneMapping)
                {
                    return questionTree;
                }
            }

            return null;
        }

        private static IReadOnlyList<IQuestion> GetAllQuestions()
        {
            return QuestionHelpers.GetAllQuestions(MaxQuestionDepth).ToList();
        }

        private static IEnumerable<IQuestion> GetAllSecondQuestions()
        {
            //return QuestionHelpers.GetAllQuestions(2);
            return new[]
            {
                new IsAnswerQuestion(Position.Two, new IsItQuestion(Position.Two, Position.One, Robot.R), Word.Ozo)
            };
        }

        private static IEnumerable<IQuestion> GetAllThirdQuestions()
        {
            //return QuestionHelpers.GetAllQuestions(2);
            return new[]
            {
                new IsAnswerQuestion(Position.Two, new IsItQuestion(Position.Two, Position.Two, Robot.R), Word.Ozo)
            };
        }

        private static IDictionary<Word, IList<ProblemMapping>> GetQuestionResults(IQuestion question, IEnumerable<ProblemMapping> problemMappings = null)
        {
            problemMappings = problemMappings ?? ProblemMapping.AllProblemMappings;

            var wordToMapping = WordHelpers.AllWords()
                .ToDictionary<Word, Word, IList<ProblemMapping>>(word => word, word => new List<ProblemMapping>());

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

        private static void AnalyseResults(IDictionary<Word, IList<ProblemMapping>> results)
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
