using AutoMapper;
using SizeFintech.Communication.Responses;
using SizeFintech.Domain.Repositories.Anticipations;
using SizeFintech.Domain.Services.LoggedUser;

namespace SizeFintech.Application.UseCases.Anticipations.GetAll;
public class GetAllAnticipationsUseCase : IGetAllAnticipationsUseCase
{
    private readonly IAnticipationsReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetAllAnticipationsUseCase(
        IAnticipationsReadOnlyRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseAnticipationsJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetAll(loggedUser);

        return new ResponseAnticipationsJson
        {
            Anticipations = _mapper.Map<List<ResponseShortAnticipationJson>>(result)
        };
    }
}
