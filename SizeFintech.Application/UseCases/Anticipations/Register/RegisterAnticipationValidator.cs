using FluentValidation;
using SizeFintech.Communication.Requests;
using SizeFintech.Exception;

namespace SizeFintech.Application.UseCases.Anticipations.Register;
public class RegisterAnticipationValidator : AbstractValidator<RequestRegisterAnticipationJson>
{
    public RegisterAnticipationValidator()
    {
        RuleFor(user => user.CNPJ).NotEmpty().SetValidator(new CNPJValidator<RequestRegisterAnticipationJson>());
        RuleFor(expense => expense.Invoices)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.INVOICES_MUST_BE_AT_LEAST_ONE)
            .ForEach(rule =>
                {
                    rule.SetValidator(new InvoiceValidator());
                });
    }
}
