namespace SizeFintech.Domain.Entities;
public class Anticipation
{
    public long Id { get; set; }
    public decimal? Limit { get; set; }
    public decimal NetTotal { get; set; }
    public decimal GrossTotal { get; set; }
    public ICollection<Invoice> Invoices { get; set; } = [];
    public DateTime CreatedAt { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
