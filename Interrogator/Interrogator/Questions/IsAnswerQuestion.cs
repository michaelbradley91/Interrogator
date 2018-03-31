using System;
using System.Collections.Generic;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Mappings;

namespace Interrogator.Questions
{
    public class IsAnswerQuestion : IQuestion, IComplexQuestion
    {
        public Position AddressedTo { get; }
        public IQuestion Question { get; }
        public Word Word { get; }

        public IsAnswerQuestion(Position addressedTo, IQuestion question, Word word)
        {
            AddressedTo = addressedTo;
            Question = question;
            Word = word;
        }

        public IEnumerable<Answer> GetPossibleAnswers(ProblemMapping mapping)
        {
            var answers = Question.GetPossibleAnswers(mapping);

            var results = new List<Answer>();
            foreach (var answer in answers)
            {
                switch (mapping.RobotMapping[AddressedTo])
                {
                    case Robot.T:
                        results.Add((mapping.AnswerMapping[answer] == Word).ToAnswer());
                        continue;
                    case Robot.F:
                        results.Add((mapping.AnswerMapping[answer] != Word).ToAnswer());
                        continue;
                    case Robot.R:
                        return AnswerHelpers.AllAnswers();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return results.Distinct();
        }

        public string Text => "Asking robot " + AddressedTo + " is the answer to '" + Question.Text + "' " + Word;

        public static IEnumerable<IQuestion> GetAllPossibleQuestions(IReadOnlyList<IQuestion> questions)
        {
            return
                from addressedTo in PositionHelpers.AllPositions()
                from question in questions
                from answer in WordHelpers.AllWords()
                select new IsAnswerQuestion(addressedTo, question, answer);
        }

        IEnumerable<IQuestion> IComplexQuestion.GetAllPossibleQuestions(IReadOnlyList<IQuestion> questions)
        {
            return GetAllPossibleQuestions(questions);
        }
    }
}
