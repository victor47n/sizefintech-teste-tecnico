using SizeFintech.Domain.Entities;

namespace SizeFintech.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    string Generate(User user);
}
