namespace SizeFintech.Communication.Responses;
public class ResponseInvoiceJson
{
    public string Number { get; set; } = string.Empty;
    public decimal NetAmount { get; set; }
    public decimal GrossAmount { get; set; }
}
