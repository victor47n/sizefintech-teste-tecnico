﻿namespace SizeFintech.Communication.Requests;
public class RequestRegisterUserJson
{
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public decimal MonthlyRevenue { get; set; }
    public long IndustryId { get; set; }
}
