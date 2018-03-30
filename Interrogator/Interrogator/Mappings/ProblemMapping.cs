using System;
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

        public static IEnumerable<ProblemMapping> AllProblemMappings => LazyAllProblemMappings.Value;

        public override string ToString()
        {
            return RobotMapping + " | " + AnswerMapping;
        }

        static ProblemMapping()
        {
            LazyAllProblemMappings = new Lazy<IEnumerable<ProblemMapping>>(GetAllProblemMappings);
        }

        private static readonly Lazy<IEnumerable<ProblemMapping>> LazyAllProblemMappings;
        private static IEnumerable<ProblemMapping> GetAllProblemMappings()
        {
            var robotMappings = RobotMapping.AllRobotMappings;
            var answerMappings = AnswerMapping.AllAnswerMappings;

            return
                from r in robotMappings
                from a in answerMappings
                select new ProblemMapping(r, a);
        }
    }
}
