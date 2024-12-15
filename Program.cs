using Serilog;
using System;
using TodoApplikasjonAPIEntityDelTre.Middleware;
using TodoApplikasjonAPIEntityDelTre.Services;
using TodoApplikasjonAPIEntityDelTre.Data;
using Microsoft.EntityFrameworkCore;
using TodoApplikasjonAPIEntityDelTre.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITodoDataRepository, TodoDataRepository>();
builder.Services.AddScoped<ICategoryDataRepository, CategoryDataRepository>();

// Configure DbContext with SQLite
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-old.txt", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: null, retainedFileCountLimit: null, shared: true)
    .CreateLogger();

builder.Host.UseSerilog(); // Register Serilog with the host

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS by adding this line
app.UseCors("AllowAll");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging(); // This should now work as expected

app.MapControllers();

app.Run();
