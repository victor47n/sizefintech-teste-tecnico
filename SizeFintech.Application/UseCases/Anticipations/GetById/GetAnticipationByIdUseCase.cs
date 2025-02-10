using AutoMapper;
using SizeFintech.Communication.Responses;
using SizeFintech.Domain.Repositories.Anticipations;
using SizeFintech.Domain.Services.LoggedUser;
using SizeFintech.Exception.ExceptionsBase;
using SizeFintech.Exception;

namespace SizeFintech.Application.UseCases.Anticipations.GetById;
public class GetAnticipationByIdUseCase : IGetAnticipationByIdUseCase
{
    private readonly IAnticipationsReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetAnticipationByIdUseCase(
        IAnticipationsReadOnlyRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseAnticipationJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetById(loggedUser, id);

        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.ANTICIPATION_NOT_FOUND);
        }

        var response = _mapper.Map<ResponseAnticipationJson>(result);
        response.Company = loggedUser.Name;
        response.CNPJ = loggedUser.CNPJ;

        return response;
    }
}
