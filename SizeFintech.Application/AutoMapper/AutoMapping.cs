using AutoMapper;
using SizeFintech.Communication.Requests;
using SizeFintech.Communication.Responses;
using SizeFintech.Domain.Entities;

namespace SizeFintech.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterAnticipationJson, Anticipation>()
            .ForMember(dest => dest.Invoices, config => config.MapFrom(source => source.Invoices.Distinct()));

        CreateMap<RequestRegisterUserJson, User>();
        CreateMap<RequestInvoiceJson, Invoice>();
    }

    private void EntityToResponse()
    {
        CreateMap<Anticipation, ResponseAnticipationJson>();
        CreateMap<Anticipation, ResponseShortAnticipationJson>()
            .ForMember(dest => dest.InvoiceCount, opt => opt.MapFrom(src => src.Invoices.Count));

        CreateMap<AnticipationLimit, ResponseAnticipationLimitJson>();
        CreateMap<Industry, ResponseIndustryJson>();
        CreateMap<Invoice, ResponseInvoiceJson>();
        CreateMap<User, ResponseUserProfileJson>();
    }
}
