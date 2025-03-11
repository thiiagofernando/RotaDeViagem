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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .WithExposedHeaders("Content-Type");
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
