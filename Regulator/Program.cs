using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Regulator;
using Regulator.Configuration;
using Regulator.Json.Appsettings;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddMyServices();

builder.Services.Configure<PositionOption>(builder.Configuration.GetSection(nameof(PositionOption)));


builder.Services.AddSingleton<IPLCConfigurationProvider, PlcConfigurationProvider>();
builder.Services.AddTransient<Func<Task<PLCConfig>>>(p =>
{
    var plcConfigurationProvider = p.GetService<IPLCConfigurationProvider>();
    return plcConfigurationProvider.GetPLCConfig;
});


//builder.Services.AddTransient<IRepository<Module, Guid>, ModuleRepository>();
//builder.Services.AddTransient<IRepository<User, Guid>, UserRepository>();

builder.Services.Scan(scan =>
    scan.FromAssemblyOf<Program>()
        .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
        .AsImplementedInterfaces()
        .WithTransientLifetime()
);


builder.Services.AddSingleton<DbContext>();
builder.Services.AddTransient<ServiceA>();

var app = builder.Build();

//var service = app.Services.GetService<ServiceA>();
//await service.Test();    

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();