namespace SizeFintech.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithCNPJ(string cnpj);
    Task<Entities.User?> GetUserByCNPJ(string cnpj);
}
