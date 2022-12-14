using System.Security.AccessControl;
using DbUp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using QAndABackEnd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using QAndABackEnd.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddAuthorization(options => 
    {
        options.AddPolicy("MustBeQuestionAuthor", policy => policy.Requirements.Add(new MustBeQuestionAuthor()));
    }
);
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options => 
    options.AddPolicy("CorsPolicy", builder => 
        builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(Configuration["Frontend"])
    )
);
builder.Services.AddScoped<IAuthorizationHandler, MustBeQuestionAuthorHandler>();
builder.Services.AddSingleton<IQuestionCache, QuestionCache>();
builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(options => 
    {
        options.Authority = builder.Configuration["Auth0:Authority"];
        options.Audience = builder.Configuration["Auth0:Audience"];
    }
);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
EnsureDatabase.For.SqlDatabase(connectionString);
var upgrader = DeployChanges.To
                    .SqlDatabase(connectionString, null)
                    .WithScriptsEmbeddedInAssembly(System.Reflection.Assembly.GetExecutingAssembly())
                    .WithTransaction()
                    .Build();

if (upgrader.IsUpgradeRequired())
{
    upgrader.PerformUpgrade();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
