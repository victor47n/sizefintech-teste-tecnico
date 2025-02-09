using SizeFintech.Communication.Requests;
using SizeFintech.Communication.Responses;
using SizeFintech.Domain.Repositories.User;
using SizeFintech.Domain.Security.Tokens;
using SizeFintech.Exception.ExceptionsBase;
using System.Text.RegularExpressions;

namespace SizeFintech.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IAccessTokenGenerator accessTokenGenerator
        )
    {
        _repository = repository;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        request.CNPJ = Regex.Replace(request.CNPJ, @"\D", "");

        Validate(request);

        var user = await _repository.GetUserByCNPJ(request.CNPJ);

        if (user is null)
        {
            throw new InvalidLoginException();
        }

        
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user)
        };
    }

    private static void Validate(RequestLoginJson request)
    {
        var result = new DoLoginValidator().Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
