using Domain.Abstractions;
using Infrastructure.BackgroundWorkers;
using Infrastructure.Context;
using Infrastructure.ExternalServices;
using Infrastructure.Queues;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer");
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddSingleton<IPersonRegistrationQueue, PersonRegistrationQueue>();
        services.AddHostedService<PersonRegistrationWorker>();
        services.AddSingleton<IExternalPartnerService, ExternalPartnerService>();
        services.AddSingleton<ISecondExternalPartnerService, SecondExternalPartnerService>();

    }
}