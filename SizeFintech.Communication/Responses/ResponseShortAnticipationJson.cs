namespace SizeFintech.Communication.Responses;
public class ResponseShortAnticipationJson
{
    public long Id { get; set; }
    public decimal NetTotal { get; set; }
    public decimal GrossTotal { get; set; }
    public int InvoiceCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
