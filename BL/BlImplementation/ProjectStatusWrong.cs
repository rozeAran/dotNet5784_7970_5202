using System.Runtime.Serialization;

namespace BlImplementation
{
    [Serializable]
    internal class ProjectStatusWrong : Exception
    {
        public ProjectStatusWrong()
        {
        }

        public ProjectStatusWrong(string? message) : base(message)
        {
        }

        public ProjectStatusWrong(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ProjectStatusWrong(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}