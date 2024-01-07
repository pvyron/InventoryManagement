using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InMa.DataAccess;

public static class DataAccessInstaller
{
    public static IHostApplicationBuilder AddMasterDbContext(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<MasterDbContext>(optionsAction: options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("InMaster"));
        });
        builder.Services.AddScoped<IMasterDbContext>(provider => provider.GetService<MasterDbContext>()!);

        return builder;
    }
}