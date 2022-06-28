namespace CustomExceptions;

public class InvalidCredentialsException : System.Exception
{
    public InvalidCredentialsException() { }
    public InvalidCredentialsException(string message) : base(message) { }
    public InvalidCredentialsException(string message, System.Exception inner) : base(message, inner) { }
    protected InvalidCredentialsException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}