using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SizeFintech.Domain.Repositories;
using SizeFintech.Domain.Repositories.Anticipations;
using SizeFintech.Domain.Repositories.Industries;
using SizeFintech.Domain.Repositories.Invoices;
using SizeFintech.Domain.Repositories.User;
using SizeFintech.Domain.Security.Tokens;
using SizeFintech.Domain.Services.LoggedUser;
using SizeFintech.Infrastructure.DataAccess;
using SizeFintech.Infrastructure.DataAccess.Repositories;
using SizeFintech.Infrastructure.Security.Tokens;
using SizeFintech.Infrastructure.Services.LoggedUser;

namespace SizeFintech.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();

        AddToken(services, configuration);
        AddRepositories(services);

        AddDbContext(services, configuration);
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IIndustriesReadOnlyRepository, IndustriesRepository>();
        services.AddScoped<IAnticipationWriteOnlyRepository, AnticipationsRepository>();
        services.AddScoped<IAnticipationsReadOnlyRepository, AnticipationsRepository>();
        services.AddScoped<IInvoicesWriteOnlyRepository, InvoicesRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddDbContext<SizeFintechDbContext>(config => config.UseSqlServer(connectionString));
    }
}
