using System;
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

        public string GetText(int indent = 0)
        {
            return string.Join("", Enumerable.Repeat(" ", indent)) + Question.Text + "? " + (Children.Any() ? Environment.NewLine + string.Join(Environment.NewLine, Children.Select(kvp => string.Join("", Enumerable.Repeat(" ", indent)) + kvp.Key + " => " + Environment.NewLine + kvp.Value.GetText(indent + 2))) : "");
        }
    }
}
