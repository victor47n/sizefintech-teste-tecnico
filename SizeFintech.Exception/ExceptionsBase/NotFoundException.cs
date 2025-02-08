using System.Net;

namespace SizeFintech.Exception.ExceptionsBase;

public class NotFoundException(string message) : SizeFintechException(message)
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
