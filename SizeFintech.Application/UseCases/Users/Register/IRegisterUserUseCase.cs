using SizeFintech.Communication.Requests;
using SizeFintech.Communication.Responses;

namespace SizeFintech.Application.UseCases.Users.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
