using SizeFintech.Domain.Entities;
using SizeFintech.Domain.Repositories.Invoices;

namespace SizeFintech.Infrastructure.DataAccess.Repositories;
internal class InvoicesRepository : IInvoicesWriteOnlyRepository
{
    private readonly SizeFintechDbContext _dbContext;

    public InvoicesRepository(SizeFintechDbContext dbContext) => _dbContext = dbContext;

    public async Task AddAll(ICollection<Invoice> invoice)
    {
        await _dbContext.Invoices.AddRangeAsync(invoice);
    }
}
