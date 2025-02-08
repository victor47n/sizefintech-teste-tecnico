using FluentValidation;
using FluentValidation.Validators;
using SizeFintech.Exception;

namespace SizeFintech.Application.UseCases;
public partial class CNPJValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";

    public override string Name => "CNPJValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.CNPJ_INVALID);
            return false;
        }

        if (cnpj.Length != 14)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.CNPJ_INVALID);
            return false;
        }

        string[] invalidNumbers = [
            "00000000000000", "11111111111111", "22222222222222",
            "33333333333333", "44444444444444", "55555555555555",
            "66666666666666", "77777777777777", "88888888888888", "99999999999999"
        ];

        if (invalidNumbers.Contains(cnpj))
            return false;

        if (!ValidateCnpjCheckDigits(cnpj))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.CNPJ_INVALID);
            return false;
        }

        return true;
    }

    public static bool ValidateCnpjCheckDigits(string cnpj)
    {
        int[] multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
        int sum = 0;

        for (int i = 0; i < 12; i++)
            sum += (tempCnpj[i] - '0') * multiplier1[i];

        int remainder = sum % 11;
        int checkDigit1 = remainder < 2 ? 0 : 11 - remainder;

        tempCnpj += checkDigit1;
        sum = 0;

        for (int i = 0; i < 13; i++)
            sum += (tempCnpj[i] - '0') * multiplier2[i];

        remainder = sum % 11;
        int checkDigit2 = remainder < 2 ? 0 : 11 - remainder;

        return cnpj.EndsWith(checkDigit1.ToString() + checkDigit2.ToString());
    }
}
