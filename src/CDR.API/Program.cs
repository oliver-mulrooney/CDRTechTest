using CDR.Data;
using CDR.Service.Extensions;
using CDR.Data.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CDRContext>(options =>
{
    var serviceOptions = builder.Configuration["CDRConnectionString"];

    if (string.IsNullOrEmpty(serviceOptions))
    {
        throw new Exception("CDRConnectionString was not found");
    }

    options
    .UseLazyLoadingProxies()
    .UseMySQL(serviceOptions!);
});

builder.Services.AddCdrService(); 
builder.Services.AddCdrData();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
