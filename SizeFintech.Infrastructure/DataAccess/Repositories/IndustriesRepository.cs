using Microsoft.EntityFrameworkCore;
using SizeFintech.Domain.Entities;
using SizeFintech.Domain.Repositories.Industries;

namespace SizeFintech.Infrastructure.DataAccess.Repositories;
internal class IndustriesRepository : IIndustriesReadOnlyRepository
{
    private readonly SizeFintechDbContext _dbContext;

    public IndustriesRepository(SizeFintechDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<Industry>> GetAll()
    {
        return await _dbContext.Industries
            .AsNoTracking()
            .Include(industry => industry.AnticipationLimits)
            .ToListAsync();
    }

    public async Task<Industry?> GetById(long id)
    {
        return await _dbContext
            .Industries
            .AsNoTracking()
            .Include(industry => industry.AnticipationLimits)
            .FirstOrDefaultAsync(industry => industry.Id.Equals(id));
    }
}
