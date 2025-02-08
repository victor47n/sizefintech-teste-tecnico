using FluentValidation;
using SizeFintech.Communication.Requests;
using SizeFintech.Exception;

namespace SizeFintech.Application.UseCases.Users.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        RuleFor(user => user.CNPJ).NotEmpty().SetValidator(new CNPJValidator<RequestRegisterUserJson>());
        RuleFor(user => user.MonthlyRevenue).NotEmpty().WithMessage(ResourceErrorMessages.MONTHLY_REVENUE_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(user => user.IndustryId).NotEmpty().WithMessage(ResourceErrorMessages.INDUSTRY_EMPTY);
    }
}
