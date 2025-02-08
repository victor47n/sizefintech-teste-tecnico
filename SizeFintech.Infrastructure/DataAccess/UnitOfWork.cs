using SizeFintech.Domain.Repositories;

namespace SizeFintech.Infrastructure.DataAccess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly SizeFintechDbContext _dbContext;

    public UnitOfWork(SizeFintechDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
