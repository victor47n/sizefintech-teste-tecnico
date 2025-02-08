using SizeFintech.Domain.Entities;

namespace SizeFintech.Domain.Repositories.Anticipations;
public interface IAnticipationWriteOnlyRepository
{
    Task Add(Anticipation anticipation);
}
