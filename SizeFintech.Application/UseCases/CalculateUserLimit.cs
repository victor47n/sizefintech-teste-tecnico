using SizeFintech.Domain.Entities;

namespace SizeFintech.Application.UseCases;
public static class CalculateUserLimit
{
    public static decimal Get(decimal monthlyRevenue, ICollection<AnticipationLimit> anticipationLimits)
    {
        var anticipationLimit =  anticipationLimits
        .Where(limit => monthlyRevenue >= limit.RevenueMinimun && (limit.RevenueMaximum == null || monthlyRevenue <= limit.RevenueMaximum))
        .OrderByDescending(limit => limit.RevenueMinimun)
        .First();

        return monthlyRevenue * anticipationLimit.AnticipationPercent;
    }
}
