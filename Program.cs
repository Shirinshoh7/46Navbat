using Microsoft.EntityFrameworkCore;
using _46Navbat.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. База данных
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- ИСПРАВЛЕНИЕ №1: Добавляем CORS ---
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// --- ИСПРАВЛЕНИЕ №2: Включаем статические файлы (для HTML) ---
app.UseDefaultFiles(); // Чтобы сервер искал index.html сам
app.UseStaticFiles();  // Чтобы сервер мог отдать CSS и JS

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Включаем CORS
app.UseCors();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();