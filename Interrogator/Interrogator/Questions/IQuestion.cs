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
        IEnumerable<IQuestion> GetAllPossibleQuestions(IReadOnlyList<IQuestion> questions);
    }

    public static class QuestionHelpers
    {
        public static IEnumerable<IQuestion> GetAllQuestions(int depth)
        {
            if (QuestionsByDepth.ContainsKey(depth)) return QuestionsByDepth[depth];

            if (depth <= 0) return new List<IQuestion>();
            if (depth == 1)
            {
                var allQuestions = ReflectionHelpers.AllSimpleQuestions.SelectMany(q =>
                        q.GetMethod("GetAllPossibleQuestions").Invoke(null, null) as IEnumerable<IQuestion>).ToList();

                QuestionsByDepth.Add(depth, allQuestions);
                return allQuestions;
            }

            var allSimplerQuestions = GetAllQuestions(depth - 1);
            var allComplexQuestions = ReflectionHelpers.AllComplexQuestions.SelectMany(q =>
                q.GetMethod("GetAllPossibleQuestions").Invoke(null, new object[] { allSimplerQuestions }) as IEnumerable<IQuestion>).ToList();

            QuestionsByDepth.Add(depth, allComplexQuestions);
            return allComplexQuestions;
        }

        private static readonly IDictionary<int, IList<IQuestion>> QuestionsByDepth =
            new Dictionary<int, IList<IQuestion>>();
    }
}
