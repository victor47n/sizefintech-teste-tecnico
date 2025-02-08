namespace SizeFintech.Domain.Entities;
public class AnticipationLimit
{
    public long Id { get; set; }
    public decimal RevenueMinimun {  get; set; }
    public decimal? RevenueMaximum { get; set; }
    public decimal AnticipationPercent { get; set; }

    public long IndustryId { get; set; }
    public Industry Industry { get; set; } = default!;
}
