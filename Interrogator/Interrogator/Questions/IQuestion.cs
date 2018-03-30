using Interrogator.Enumerations;
using Interrogator.Mappings;

namespace Interrogator.Questions
{
    public interface IQuestion
    {
        Answer GetAnswer(ProblemMapping mapping);
    }
}
