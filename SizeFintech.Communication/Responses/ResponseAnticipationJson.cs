namespace SizeFintech.Communication.Responses;
public class ResponseAnticipationJson
{
    public long Id { get; set; }
    public string CNPJ { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public decimal Limit { get; set; }
    public decimal NetTotal { get; set; }
    public decimal GrossTotal { get; set; }
    public List<ResponseInvoiceJson> Invoices { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}
