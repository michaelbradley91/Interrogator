using System.Collections.Generic;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Mappings;
using Interrogator.Helpers;

namespace Interrogator.Questions
{
    public interface IQuestion
    {
        Position AddressedTo { get; }
        IEnumerable<Answer> GetPossibleAnswers(ProblemMapping mapping);
        string Text { get; }
    }

    public interface ISimpleQuestion
    {
        IEnumerable<IQuestion> GetAllPossibleQuestions();
    }

    public interface IComplexQuestion
    {
        int NumberOfQuestions { get; }
        IEnumerable<IQuestion> GetAllPossibleQuestions(IReadOnlyList<IQuestion> questions);
    }

    public static class QuestionHelpers
    {
        public static IEnumerable<IQuestion> GetAllQuestions(int depth)
        {
            if (QuestionsByComplexity.ContainsKey(depth)) return QuestionsByComplexity[depth];

            if (depth == 0) return new List<IQuestion>();
            if (depth == 1)
            {
                var allQuestions = ReflectionHelpers.AllSimpleQuestions.SelectMany(q =>
                        q.GetMethod("GetAllPossibleQuestions").Invoke(null, null) as IEnumerable<IQuestion>).ToList();

                QuestionsByComplexity.Add(depth, allQuestions);
                return allQuestions;
            }

            return null;

        }

        private static readonly IDictionary<int, IList<IQuestion>> QuestionsByComplexity =
            new Dictionary<int, IList<IQuestion>>();
    }
}
