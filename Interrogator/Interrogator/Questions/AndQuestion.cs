using System;
using Interrogator.Enumerations;
using Interrogator.Mappings;

namespace Interrogator.Questions
{
    public class AndQuestion : IQuestion
    {
        public Position AddressedTo { get; }
        public IQuestion FirstQuestion { get; }
        public IQuestion SecondQuestion { get; }

        public AndQuestion(Position addressedTo, IQuestion firstQuestion, IQuestion secondQuestion)
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
                    if (firstAnswer == Answer.No || secondAnswer == Answer.No) return Answer.No;
                    if (firstAnswer == Answer.Unknown || secondAnswer == Answer.Unknown) return Answer.Unknown;
                    return Answer.Yes;
                case Robot.F:
                    if (firstAnswer == Answer.No || secondAnswer == Answer.No) return Answer.Yes;
                    // It is a bit unclear what a lie is in this case, as both yes or no are incorrect. For simplicity, assume yes.
                    if (firstAnswer == Answer.Unknown || secondAnswer == Answer.Unknown) return Answer.Yes;
                    return Answer.No;
                case Robot.R:
                    return Answer.Unknown;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
