using SizeFintech.Communication.Responses;

namespace SizeFintech.Application.UseCases.Industries.GetAll;
public interface IGetAllIndustriesUseCase
{
    Task<ResponseIndustriesJson> Execute();
}
