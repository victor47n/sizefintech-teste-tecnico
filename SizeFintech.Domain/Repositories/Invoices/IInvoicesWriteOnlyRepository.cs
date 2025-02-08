using SizeFintech.Domain.Entities;

namespace SizeFintech.Domain.Repositories.Invoices;
public interface IInvoicesWriteOnlyRepository
{
    Task AddAll(ICollection<Invoice> invoice);
}
