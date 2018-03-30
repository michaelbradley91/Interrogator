using System.Collections.Generic;
using System.Linq;

namespace Interrogator.Mappings
{
    public class ProblemMapping
    {
        public RobotMapping RobotMapping { get; }
        public AnswerMapping AnswerMapping { get; }

        public ProblemMapping(RobotMapping robotMapping, AnswerMapping answerMapping)
        {
            RobotMapping = robotMapping;
            AnswerMapping = answerMapping;
        }

        public static IEnumerable<ProblemMapping> GetAllProblemMappings()
        {
            var robotMappings = RobotMapping.GetAllRobotMappings();
            var answerMappings = AnswerMapping.GetAllAnswerMappings();

            return from r in robotMappings
                   from a in answerMappings
                   select new ProblemMapping(r, a);
        }

        public override string ToString()
        {
            return RobotMapping + " | " + AnswerMapping;
        }
    }
}
