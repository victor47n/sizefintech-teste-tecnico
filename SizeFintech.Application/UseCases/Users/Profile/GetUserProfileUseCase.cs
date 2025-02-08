using AutoMapper;
using SizeFintech.Communication.Responses;
using SizeFintech.Domain.Services.LoggedUser;

namespace SizeFintech.Application.UseCases.Users.Profile;
public class GetUserProfileUseCase : IGetUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await _loggedUser.Get();

        try
        {
            return _mapper.Map<ResponseUserProfileJson>(user);
        }
        catch
        {
            throw;
        }
    }
}
