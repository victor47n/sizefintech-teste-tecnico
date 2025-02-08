using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SizeFintech.Infrastructure.DataAccess;

namespace SizeFintech.Infrastructure.Migrations;
public static class DataBaseMigration
{
    public async static Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<SizeFintechDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
