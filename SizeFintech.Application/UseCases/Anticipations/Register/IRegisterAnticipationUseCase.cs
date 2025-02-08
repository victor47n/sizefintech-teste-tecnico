using SizeFintech.Communication.Requests;

namespace SizeFintech.Application.UseCases.Anticipations.Register;
public interface IRegisterAnticipationUseCase
{
    Task Execute(RequestRegisterAnticipationJson request);
}
