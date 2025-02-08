using FluentValidation;
using SizeFintech.Communication.Requests;
using SizeFintech.Exception;

namespace SizeFintech.Application.UseCases.Anticipations.Register;
public class InvoiceValidator : AbstractValidator<RequestInvoiceJson>
{
    public InvoiceValidator()
    {
        RuleFor(i => i.Number)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.INVOICE_NUMBER_EMPTY);

        RuleFor(i => i.GrossAmount)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.GROSS_AMOUNT_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(i => i.DueDate)
            .GreaterThan(DateTime.Today)
            .WithMessage(ResourceErrorMessages.DUE_DATE_MUST_BE_IN_THE_FUTURE);
    }
}
