namespace SizeFintech.Communication.Requests;
public class RequestRegisterAnticipationJson
{
    public ICollection<RequestInvoiceJson> Invoices { get; set; } = [];
}
