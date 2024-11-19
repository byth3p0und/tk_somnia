using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tk.Somnia.Data.Context.Journals;
using Tk.Somnia.Data.Context.Journals.Interceptors;
using Tk.Somnia.Data.Repositories;
using Tk.Somnia.Interface.Interceptors;
using Tk.Somnia.Interface.Repositories;

namespace Tk.Somnia.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<JournalDbContext>(o => o
                .UseSqlServer(configuration.GetConnectionString("Journal"))
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
            );

        services.AddScoped<IPersistenceInterceptor, PersistenceInterceptor>();

        services.AddScoped<IJournalRepository, JournalRepository>();
        
        return services;
    }
}