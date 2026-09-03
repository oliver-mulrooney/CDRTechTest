using CDR.Service.Mappers;
using CDR.Service.Mappers.Interfaces;
using CDR.Service.Services;
using CDR.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CDR.Service.Extensions;
public static class CDRServiceCollectionExtensions
{
    public static IServiceCollection AddCdrService(this IServiceCollection services)
    {
        //Register Interfaces for DI
        services.AddScoped<ICDRService, CDRService>();
        services.AddScoped<ICDRUploadSummaryMapper, CDRUploadSummaryMapper>();
        services.AddScoped<ICDRCsvRecordsToEntitiesMapper, CDRCsvRecordsToEntitiesMapper>();

        return services;
    }
}
