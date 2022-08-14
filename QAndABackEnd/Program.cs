using System.Security.AccessControl;
using DbUp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
EnsureDatabase.For.SqlDatabase(connectionString);
var upgrader = DeployChanges.To
                    .SqlDatabase(connectionString, null)
                    .WithScriptsEmbeddedInAssembly(System.Reflection.Assembly.GetExecutingAssembly())
                    .WithTransaction()
                    .Build();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
