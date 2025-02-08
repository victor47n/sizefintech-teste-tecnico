using SizeFintech.Domain.Entities;

namespace SizeFintech.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> Get();
}
