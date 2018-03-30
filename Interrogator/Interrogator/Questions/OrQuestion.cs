using System;
using System.Collections.Generic;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Mappings;

namespace Interrogator.Questions
{
    public class OrQuestion : IQuestion
    {
        public Position AddressedTo { get; }
        public IQuestion FirstQuestion { get; }
        public IQuestion SecondQuestion { get; }

        public OrQuestion(Position addressedTo, IQuestion firstQuestion, IQuestion secondQuestion)
        {
            AddressedTo = addressedTo;
            FirstQuestion = firstQuestion;
            SecondQuestion = secondQuestion;
        }

        public IEnumerable<Answer> GetPossibleAnswers(ProblemMapping mapping)
        {
            var firstAnswers = FirstQuestion.GetPossibleAnswers(mapping).ToList();
            var secondAnswers = SecondQuestion.GetPossibleAnswers(mapping).ToList();

            var answers = new List<Answer>();
            foreach (var firstAnswer in firstAnswers)
            {
                foreach (var secondAnswer in secondAnswers)
                {
                    switch (mapping.RobotMapping[AddressedTo])
                    {
                        case Robot.T:
                            answers.Add((firstAnswer == Answer.Yes || secondAnswer == Answer.Yes).ToAnswer());
                            continue;
                        case Robot.F:
                            answers.Add((firstAnswer != Answer.Yes && secondAnswer != Answer.Yes).ToAnswer());
                            continue;
                        case Robot.R:
                            return AnswerHelpers.AllAnswers();
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return answers.Distinct();
        }
    }
}
