namespace QuizCore.Common.Exceptions{
    public class TokenValidationException : Exception
    {
        public TokenValidationException()
           : base()
        {
        }

        public TokenValidationException(string message)
            : base(message)
        {
        }

        public TokenValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public TokenValidationException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
