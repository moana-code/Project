using Microsoft.EntityFrameworkCore;
using Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicionar DbContext e CORS
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("GestorTarefas"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Adicionar controladores
builder.Services.AddControllers();

var app = builder.Build();

// Usar CORS e Controladores
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
