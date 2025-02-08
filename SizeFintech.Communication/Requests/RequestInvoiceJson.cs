namespace SizeFintech.Communication.Requests;
public class RequestInvoiceJson
{
    public string Number { get; set; } = string.Empty;
    public decimal GrossAmount { get; set; }
    public DateTime DueDate { get; set; }
}
