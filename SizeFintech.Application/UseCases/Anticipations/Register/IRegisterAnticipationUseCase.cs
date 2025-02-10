using SizeFintech.Communication.Requests;
using SizeFintech.Communication.Responses;

namespace SizeFintech.Application.UseCases.Anticipations.Register;
public interface IRegisterAnticipationUseCase
{
    Task<ResponseAnticipationJson> Execute(RequestRegisterAnticipationJson request);
}
