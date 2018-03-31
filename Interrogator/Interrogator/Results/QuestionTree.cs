using System.Collections.Generic;
using System.Linq;
using Interrogator.Enumerations;
using Interrogator.Questions;

namespace Interrogator.Results
{
    public class QuestionTree
    {
        public IQuestion Question { get; }

        public QuestionTree(IQuestion question)
        {
            Question = question;
        }

        public IDictionary<Word, QuestionTree> Children { get; } = new Dictionary<Word, QuestionTree>();

        public override string ToString()
        {
            return Question.Text + "? [" + string.Join(" | ", Children.Select(kvp => kvp.Key + " => " + kvp.Value)) + "]";
        }
    }
}
