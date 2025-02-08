using SizeFintech.Domain.Entities;

namespace SizeFintech.Domain.Repositories.Industries;
public interface IIndustriesReadOnlyRepository
{
    Task<List<Industry>> GetAll();
    Task<Industry?> GetById(long id);
}
