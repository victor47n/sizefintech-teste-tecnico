namespace SizeFintech.Domain.Entities;
public class User
{
    public long Id { get; set; }
    public Guid UserIdentifier { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public decimal MonthlyRevenue { get; set; }
    public ICollection<Anticipation> Anticipations { get; set; } = [];

    public long IndustryId { get; set; }
    public Industry Industry { get; set; } = default!;
}
