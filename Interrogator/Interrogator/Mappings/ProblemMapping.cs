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
    }
}
