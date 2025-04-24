using Microsoft.EntityFrameworkCore; 
using TarefasApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 👉 Adicione isso aqui:
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Tarefas.db"));

var app = builder.Build();
