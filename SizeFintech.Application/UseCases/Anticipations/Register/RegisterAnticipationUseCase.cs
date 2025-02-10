using AutoMapper;
using SizeFintech.Communication.Requests;
using SizeFintech.Communication.Responses;
using SizeFintech.Domain.Entities;
using SizeFintech.Domain.Repositories;
using SizeFintech.Domain.Repositories.Anticipations;
using SizeFintech.Domain.Repositories.Industries;
using SizeFintech.Domain.Repositories.Invoices;
using SizeFintech.Domain.Services.LoggedUser;
using SizeFintech.Exception;
using SizeFintech.Exception.ExceptionsBase;
using System.Globalization;

namespace SizeFintech.Application.UseCases.Anticipations.Register;
internal class RegisterAnticipationUseCase : IRegisterAnticipationUseCase
{
    private readonly IAnticipationWriteOnlyRepository _anticipationRepository;
    private readonly IIndustriesReadOnlyRepository _industriesRepository;
    private readonly IInvoicesWriteOnlyRepository _invoicesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterAnticipationUseCase(
        IAnticipationWriteOnlyRepository anticipationRepository,
        IIndustriesReadOnlyRepository industriesRepository,
        IInvoicesWriteOnlyRepository invoicesRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser
    )
    {
        _anticipationRepository = anticipationRepository;
        _industriesRepository = industriesRepository;
        _invoicesRepository = invoicesRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseAnticipationJson> Execute(RequestRegisterAnticipationJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var anticipation = _mapper.Map<Anticipation>(request);
        anticipation.UserId = loggedUser.Id;
        anticipation.CreatedAt = DateTime.UtcNow;

        await CalculateAnticipationValues(loggedUser, anticipation);

        await _anticipationRepository.Add(anticipation);

        await _invoicesRepository.AddAll(anticipation.Invoices);

        await _unitOfWork.Commit();

        var response = _mapper.Map<ResponseAnticipationJson>(anticipation);
        response.CNPJ = loggedUser.CNPJ;
        response.Company = loggedUser.Name;

        return response;
    }

    private void Validate(RequestRegisterAnticipationJson request)
    {
        var validator = new RegisterAnticipationValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }

    private async Task CalculateAnticipationValues(User loggedUser, Anticipation anticipation)
    {
        var industry = await _industriesRepository.GetById(loggedUser.IndustryId);

        var totalGross = anticipation.Invoices.Sum(invoice => invoice.GrossAmount);

        var limit = CalculateUserLimit.Get(loggedUser.MonthlyRevenue, industry!.AnticipationLimits);

        if (totalGross > limit)
        {
            throw new ErrorOnValidationException([string.Format(
                CultureInfo.InvariantCulture,
                ResourceErrorMessages.GROSS_AMOUNT_CANNOT_EXCEED_THE_LIMIT,
                totalGross,
                limit
            )]);
        }

        decimal totalNet = 0;

        foreach (var invoice in anticipation.Invoices)
        {
            var prazoDias = (invoice.DueDate - DateTime.UtcNow.Date).Days;
            decimal netAmount = invoice.GrossAmount / (decimal)Math.Pow((double)(1 + 0.0465), prazoDias / 30.0);

            invoice.NetAmount = netAmount;
            totalNet += netAmount;
        }

        anticipation.NetTotal = totalNet;
        anticipation.GrossTotal = totalGross;
        anticipation.Limit = Math.Round(limit, 2);
    }
}
