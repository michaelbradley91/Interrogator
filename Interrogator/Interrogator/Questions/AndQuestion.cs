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
                    return mapping.AnswerMapping[firstAnswer == mapping.AnswerMapping.Yes && secondAnswer == mapping.AnswerMapping.Yes];
                case Robot.F:
                    return mapping.AnswerMapping[firstAnswer != mapping.AnswerMapping.Yes || secondAnswer != mapping.AnswerMapping.Yes];
                case Robot.R:
                    return mapping.AnswerMapping.Random();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
