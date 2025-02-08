using SizeFintech.Communication.Responses;

namespace SizeFintech.Application.UseCases.Users.Profile;
public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
