namespace SizeFintech.Domain.Entities;
public class Invoice
{
    public long Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public decimal NetAmount { get; set; }
    public decimal GrossAmount { get; set; }
    public DateTime DueDate { get; set; }

    public long AnticipationId { get; set; }
    public Anticipation Anticipation { get; set; } = default!;
}
