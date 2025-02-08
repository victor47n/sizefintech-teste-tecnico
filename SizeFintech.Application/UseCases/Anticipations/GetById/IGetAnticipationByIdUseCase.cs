using SizeFintech.Communication.Responses;

namespace SizeFintech.Application.UseCases.Anticipations.GetById;
public interface IGetAnticipationByIdUseCase
{
    Task<ResponseAnticipationJson> Execute(long id);
}
