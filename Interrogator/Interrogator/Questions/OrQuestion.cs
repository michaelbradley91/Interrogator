using System;
using System.Collections.Generic;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Mappings;

namespace Interrogator.Questions
{
    public class OrQuestion : IQuestion, IComplexQuestion
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

        public string Text => "Asking robot " + AddressedTo + " " + FirstQuestion.Text + " or " + SecondQuestion.Text;

        public static IEnumerable<IQuestion> GetAllPossibleQuestions(IReadOnlyList<IQuestion> questions)
        {
            return
                from addressedTo in PositionHelpers.AllPositions()
                from firstQuestion in questions
                from secondQuestion in questions
                select new OrQuestion(addressedTo, firstQuestion, secondQuestion);
        }

        IEnumerable<IQuestion> IComplexQuestion.GetAllPossibleQuestions(IReadOnlyList<IQuestion> questions)
        {
            return GetAllPossibleQuestions(questions);
        }
    }
}
