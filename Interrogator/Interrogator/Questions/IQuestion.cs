using System.Collections.Generic;
using Interrogator.Enumerations;
using Interrogator.Mappings;

namespace Interrogator.Questions
{
    public interface IQuestion
    {
        Position AddressedTo { get; }
        IEnumerable<Answer> GetPossibleAnswers(ProblemMapping mapping);
    }
}
