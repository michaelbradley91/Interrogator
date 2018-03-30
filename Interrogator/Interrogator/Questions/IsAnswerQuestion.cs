using System;
using System.Collections.Generic;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Mappings;

namespace Interrogator.Questions
{
    public class IsAnswerQuestion : IQuestion
    {
        public Position AddressedTo { get; }
        public IQuestion Question { get; }
        public Language Word { get; }

        public IsAnswerQuestion(Position addressedTo, IQuestion question, Language word)
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
    }
}
