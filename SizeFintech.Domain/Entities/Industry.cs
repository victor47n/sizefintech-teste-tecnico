namespace SizeFintech.Domain.Entities;
public class Industry
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<AnticipationLimit> AnticipationLimits { get; set; } = [];
}
