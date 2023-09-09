using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MonthBudget.Data;
using MonthBudget.Data.Repositories;
using MonthBudget.ServiceContracts;
using MonthBudget.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the IoC container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MonthBudget API", Version = "v1" });
});

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ExpensesRepository>();
builder.Services.AddScoped<IExpensesService, ExpensesService>();
builder.Services.AddDbContext<MonthBudgetDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed((host) => true)
    .AllowCredentials());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
