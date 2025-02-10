namespace SizeFintech.Communication.Responses;
public class ResponseUserProfileJson
{
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public decimal MonthlyRevenue { get; set; }
    public decimal Limit { get; set; }
    public long IndustryId { get; set; }
}
