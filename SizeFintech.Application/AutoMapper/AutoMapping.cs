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
        CreateMap<Anticipation, ResponseAnticipationJson>()
            .ForMember(dest => dest.Limit, opt => opt.MapFrom(src => src.Limit.HasValue ? Math.Round(src.Limit.Value, 2) : (decimal?)null))
            .ForMember(dest => dest.NetTotal, opt => opt.MapFrom(src => Math.Round(src.NetTotal, 2)))
            .ForMember(dest => dest.GrossTotal, opt => opt.MapFrom(src => Math.Round(src.GrossTotal, 2)));

        CreateMap<Anticipation, ResponseShortAnticipationJson>()
            .ForMember(dest => dest.InvoiceCount, opt => opt.MapFrom(src => src.Invoices.Count));

        CreateMap<AnticipationLimit, ResponseAnticipationLimitJson>();
        CreateMap<Industry, ResponseIndustryJson>();
        CreateMap<Invoice, ResponseInvoiceJson>()
            .ForMember(dest => dest.NetAmount, opt => opt.MapFrom(src => Math.Round(src.NetAmount, 2)))
            .ForMember(dest => dest.GrossAmount, opt => opt.MapFrom(src => Math.Round(src.GrossAmount, 2)));
        CreateMap<User, ResponseUserProfileJson>();
    }
}
