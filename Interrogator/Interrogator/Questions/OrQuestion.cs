using System;
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

        public Answer GetAnswer(ProblemMapping mapping)
        {
            var firstAnswer = FirstQuestion.GetAnswer(mapping);
            var secondAnswer = SecondQuestion.GetAnswer(mapping);

            switch (mapping.RobotMapping[AddressedTo])
            {
                case Robot.T:
                    if (firstAnswer == Answer.Yes || secondAnswer == Answer.Yes) return Answer.Yes;
                    if (firstAnswer == Answer.Unknown || secondAnswer == Answer.Unknown) return Answer.Unknown;
                    return Answer.No;
                case Robot.F:
                    if (firstAnswer == Answer.Yes || secondAnswer == Answer.Yes) return Answer.No;
                    // It is difficult to say what is false in this case, as both yes and no are incorrect. We'll use yes for simplicity.
                    if (firstAnswer == Answer.Unknown || secondAnswer == Answer.Unknown) return Answer.Yes;
                    return Answer.Yes;
                case Robot.R:
                    return Answer.Unknown;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
