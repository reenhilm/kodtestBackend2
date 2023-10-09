using backend.Core.Repositories;
using backend.Data.AutoMapper;
using backend.Data.Context;
using backend.Data.Repositories;
using backend.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using backend.Extensions;
using backend.ResultFilter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BackendDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? throw new System.InvalidOperationException("Connection string 'Default', the BackendDbContext not found."))
    );


builder.Services.AddCors(options =>
{
    options.AddPolicy(AppArgs.CORS_POLICY,
        builder =>
        {
            builder
            .WithOrigins("https://localhost:44366/", "http://localhost:44366")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithMethods("PUT", "DELETE", "GET");
        });
});

//ToDo register Automapper

builder.Services.AddControllers(opt => {
    opt.ReturnHttpNotAcceptable = true;
    opt.Filters.Add(new MyApiResultFilter());
})
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Seed data
await app.SeedDataAsync();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(AppArgs.CORS_POLICY);

app.Run();