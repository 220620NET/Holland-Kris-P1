namespace CustomExceptions
{
    /// <summary>
    /// Custom Exception for usernames that don't exist or are duplicates
    /// </summary>
    public class UsernameNotAvailable : System.Exception
    {
        public UsernameNotAvailable() { }
        public UsernameNotAvailable(string message) : base(message) { }
        public UsernameNotAvailable(string message, System.Exception inner) : base(message, inner) { }
        protected UsernameNotAvailable(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Custom Exception for errors caught in the database, that are user error
    /// </summary>
    public class ResourceNotFoundException : System.Exception
    {
        public ResourceNotFoundException() { }
        public ResourceNotFoundException(string message) : base(message) { }
        public ResourceNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected ResourceNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Custom Exception for catching improper passwords and usernames
    /// </summary>
    public class InvalidCredentialsException : System.Exception
    {
        public InvalidCredentialsException() { }
        public InvalidCredentialsException(string message) : base(message) { }
        public InvalidCredentialsException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidCredentialsException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}