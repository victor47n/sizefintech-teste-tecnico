
using System.Net;

namespace SizeFintech.Exception.ExceptionsBase;
public class InvalidLoginException : SizeFintechException
{
    public InvalidLoginException() : base(ResourceErrorMessages.CNPJ_NOT_REGISTERED) { }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
