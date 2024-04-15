namespace Zadanie3.Exceptions;

public class ContainerNotFound:Exception
{
    public ContainerNotFound(string? message) : base(message)
    {
    }
}