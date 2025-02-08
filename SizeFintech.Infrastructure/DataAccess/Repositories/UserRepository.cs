using Microsoft.EntityFrameworkCore;
using SizeFintech.Domain.Entities;
using SizeFintech.Domain.Repositories.User;

namespace SizeFintech.Infrastructure.DataAccess.Repositories;
internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly SizeFintechDbContext _dbContext;

    public UserRepository(SizeFintechDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> ExistActiveUserWithCNPJ(string cnpj)
    {
        return await _dbContext.Users.AnyAsync(user => user.CNPJ.Equals(cnpj));
    }

    public async Task<User?> GetUserByCNPJ(string cnpj)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.CNPJ.Equals(cnpj));
    }
}
