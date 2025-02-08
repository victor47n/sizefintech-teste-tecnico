namespace SizeFintech.Exception.ExceptionsBase;

public abstract class SizeFintechException(string message) : SystemException(message)
{
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
