using CDR.Data.Commands;
using CDR.Data.Commands.Interfaces;
using CDR.Data.Queries;
using CDR.Data.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CDR.Data.Extensions;
public static class CDRDataCollectionExtensions
{
    public static IServiceCollection AddCdrData(this IServiceCollection services)
    {
        //Register Interfaces for DI
        services.AddScoped<IAddCDRCommand, AddCDRCommand>();
        services.AddScoped<IGetCDRByReferenceQuery, GetCDRByReferenceQuery>();
        services.AddScoped<IGetCDRsByDateRangeQuery, GetCDRsByDateRangeQuery>();
        services.AddScoped<IGetCDRsByDateRangeAndCallerIdQuery, GetCDRsByDateRangeAndCallerIdQuery>();

        return services;
    }
}
