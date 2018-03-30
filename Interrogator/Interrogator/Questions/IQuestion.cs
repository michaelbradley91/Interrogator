using Interrogator.Enumerations;
using Interrogator.Mappings;

namespace Interrogator.Questions
{
    public interface IQuestion
    {
        Position AddressedTo { get; }
        Answer GetAnswer(ProblemMapping mapping);
    }
}
