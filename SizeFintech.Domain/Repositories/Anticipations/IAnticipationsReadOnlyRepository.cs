using SizeFintech.Domain.Entities;

namespace SizeFintech.Domain.Repositories.Anticipations;
public interface IAnticipationsReadOnlyRepository
{
    Task<List<Anticipation>> GetAll(Entities.User user);

    Task<Anticipation?> GetById(Entities.User user, long id);
}
