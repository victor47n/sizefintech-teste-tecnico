using SizeFintech.Communication.Responses;

namespace SizeFintech.Application.UseCases.Anticipations.GetAll;
public interface IGetAllAnticipationsUseCase
{
    Task<ResponseAnticipationsJson> Execute();
}
