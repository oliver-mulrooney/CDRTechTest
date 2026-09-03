using CDR.Data.Commands;
using CDR.Data.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CDR.Data.Extensions;
public static class CDRDataCollectionExtensions
{
    public static IServiceCollection AddCdrData(this IServiceCollection services)
    {
        //Register Interfaces for DI
        services.AddScoped<IAddCDRCommand, AddCDRCommand>();

        return services;
    }
}
