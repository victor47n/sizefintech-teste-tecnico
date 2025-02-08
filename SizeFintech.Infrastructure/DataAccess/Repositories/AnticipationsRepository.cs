using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SizeFintech.Domain.Entities;
using SizeFintech.Domain.Repositories.Anticipations;

namespace SizeFintech.Infrastructure.DataAccess.Repositories;
internal class AnticipationsRepository : IAnticipationsReadOnlyRepository, IAnticipationWriteOnlyRepository
{
    private readonly SizeFintechDbContext _dbContext;
    public AnticipationsRepository(SizeFintechDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Anticipation anticipation)
    {
        await _dbContext.Anticipations.AddAsync(anticipation);
    }

    public async Task<List<Anticipation>> GetAll(User user)
    {
        return await GetFullAnticipation()
            .AsNoTracking()
            .Where(anticipation => anticipation.UserId == user.Id)
            .ToListAsync();
    }

    public async Task<Anticipation?> GetById(User user, long id)
    {
        return await GetFullAnticipation()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == user.Id);
    }

    private IIncludableQueryable<Anticipation, ICollection<Invoice>> GetFullAnticipation()
    {
        return _dbContext.Anticipations
            .Include(anticipation => anticipation.Invoices);
    }
}
