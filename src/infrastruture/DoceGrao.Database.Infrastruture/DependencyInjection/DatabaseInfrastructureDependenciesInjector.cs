using System;
using DoceGrao.Database.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetHacksPack.Hosting.Abstractions.Providers;

namespace DoceGrao.Database.Infrastructure.DependencyInjection
{
    public static class DatabaseInfrastructureDependenciesInjector
    {
        public static IServiceCollection AddDataBaseContext(this IServiceCollection services, Func<IConnectionStringProvider, string> connectionStringProvider)
        {
            return services
                .AddTransientUnityOfWork<ApplicationContext>()
                .AddScoped<ApplicationContext>((serviceProvider) =>
                {
                    var connectionBuilder = serviceProvider.GetService<IConnectionStringProvider>();
                    return new ApplicationContext(connectionBuilder);
                })
                .AddDbContext<ApplicationContext>((serviceProvider, opt) =>
                {
                    var connectionBuilder = serviceProvider.GetService<IConnectionStringProvider>();

                    var connectionString = connectionStringProvider(connectionBuilder);
                    opt.UseSqlServer(connectionString, dbOptions =>
                    {
                        dbOptions.CommandTimeout(500);
                        dbOptions.MigrationsHistoryTable("MigrationsHistory", "DoceGraoApi");
                    });
                }, ServiceLifetime.Scoped);
        }
    }
}
