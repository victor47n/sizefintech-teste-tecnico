namespace SizeFintech.Communication.Requests;
public class RequestRegisterAnticipationJson
{
    public string CNPJ { get; set; } = string.Empty;
    public ICollection<RequestInvoiceJson> Invoices { get; set; } = [];
}
