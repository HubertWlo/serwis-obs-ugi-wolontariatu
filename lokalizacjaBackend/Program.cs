using lokalizacjaBackend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

/* Database Context Dependency Injection */
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
//Server = 192.168.1.12,1433; Database ={DB_NAME}; User Id = sa; Password ={DB_PASSWORD};
//var connectionString = $"Data Source=lokalizacja_sql_server_container; Initial Catalog={dbName}; User Id=sa; Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=True;";
var connectionString = $"Data Source=sql_server_container; Initial Catalog={dbName}; User Id=sa; Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=True;";
builder.Services.AddDbContext<LokalizacjaDbContext>(opt => opt.UseSqlServer(connectionString));
/* ===================================== */

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
