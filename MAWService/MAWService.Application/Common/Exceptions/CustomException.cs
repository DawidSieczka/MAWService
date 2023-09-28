namespace MAWService.Application.Common.Exceptions;
public class CustomException : Exception
{
    public virtual int StatusCode { get; set; } = 500;
    public CustomException()
    {
    }
    public CustomException(string message) : base(message)
    {

    }
    public CustomException(string message, Type type) : base(message)
    {

    }
}
