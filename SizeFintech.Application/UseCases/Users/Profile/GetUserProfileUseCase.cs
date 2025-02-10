using AutoMapper;
using SizeFintech.Communication.Responses;
using SizeFintech.Domain.Repositories.Industries;
using SizeFintech.Domain.Services.LoggedUser;

namespace SizeFintech.Application.UseCases.Users.Profile;
public class GetUserProfileUseCase : IGetUserProfileUseCase
{
    private readonly IIndustriesReadOnlyRepository _industriesRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetUserProfileUseCase(IIndustriesReadOnlyRepository industriesRepository, ILoggedUser loggedUser, IMapper mapper)
    {
        _industriesRepository = industriesRepository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseUserProfileJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var industry = await _industriesRepository.GetById(loggedUser.IndustryId);

        var limit = CalculateUserLimit.Get(loggedUser.MonthlyRevenue, industry!.AnticipationLimits);

        var response = _mapper.Map<ResponseUserProfileJson>(loggedUser);
        response.Limit = Math.Round(limit, 2);

        return response;
    }
}
