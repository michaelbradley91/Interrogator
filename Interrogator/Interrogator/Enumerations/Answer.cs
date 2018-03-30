namespace Interrogator.Enumerations
{
    public enum Answer
    {
        Yes,
        No,
        Unknown
    }

    public static class AnswerHelpers
    {
        public static Answer ToAnswer(this bool b)
        {
            return b ? Answer.Yes : Answer.No;
        }
    }
}
