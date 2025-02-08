using FluentValidation;
using SizeFintech.Communication.Requests;

namespace SizeFintech.Application.UseCases.Login.DoLogin;
public class DoLoginValidator : AbstractValidator<RequestLoginJson>
{
    public DoLoginValidator()
    {
        RuleFor(user => user.CNPJ).NotEmpty().SetValidator(new CNPJValidator<RequestLoginJson>());
    }
}
