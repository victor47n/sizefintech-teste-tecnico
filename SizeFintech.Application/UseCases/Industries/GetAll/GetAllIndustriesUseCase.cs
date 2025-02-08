using AutoMapper;
using SizeFintech.Communication.Responses;
using SizeFintech.Domain.Repositories.Industries;

namespace SizeFintech.Application.UseCases.Industries.GetAll;
public class GetAllIndustriesUseCase : IGetAllIndustriesUseCase
{
    private readonly IIndustriesReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllIndustriesUseCase(
        IIndustriesReadOnlyRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseIndustriesJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseIndustriesJson
        {
            Industries = _mapper.Map<List<ResponseIndustryJson>>(result)
        };
    }
}
