using Microsoft.EntityFrameworkCore;
using RotaDeViagemApi.Data;
using RotaDeViagemApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RotaDbContext>(options =>
    options.UseInMemoryDatabase("RotaDbApi"));

builder.Services.AddScoped<RotaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
