using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PLC.ServiceA.HttpClients;
using Microsoft.Extensions.Hosting;
using PLC.ServiceA;
using PLC.ServiceA.Configurations;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<PLCReader>();
builder.Services.Configure<RegulatorConfiguration>(builder.Configuration.GetSection(nameof(RegulatorConfiguration)));

builder.Services.AddHttpClient<RegulatorClient>();


var app = builder.Build();

app.UseRouting();
// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapMetrics();
    endpoints.MapControllers();
});


app.Run();