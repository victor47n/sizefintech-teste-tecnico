namespace SizeFintech.Communication.Responses;
public class ResponseAnticipationJson
{
    public string CNPJ { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public decimal Limit { get; set; }
    public decimal NetTotal { get; set; }
    public decimal GrossTotal { get; set; }
    public List<ResponseInvoiceJson> Invoices { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}
