namespace MBox.Models.Exceptions;

public class InsufficientRightsException : CommonException
{
    public InsufficientRightsException(object response) : base("Insufficient Rights", response)
    {
    }

    public InsufficientRightsException(string message, object response) : base(message, response)
    {
    }
}