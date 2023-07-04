using Database.API.Infrastructure;
using Database.API.Models;
using Database.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Sql"));

});
var str_directory = Environment.CurrentDirectory.ToString();
str_directory = Directory.GetParent(str_directory).FullName;
str_directory = Directory.GetParent(str_directory).FullName;
str_directory += "\\Shared\\jwtconfig.json";

//var MyConfig = new ConfigurationBuilder().AddJsonFile(str_directory).Build(); 
builder.Configuration.AddJsonFile(str_directory).Build();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));
builder.Services.AddScoped(typeof(IDatabaseService), typeof(DatabaseService));

//string authenticationProviderKey = "TestKey";

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(authenticationProviderKey, options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = "https://localhost",
//        ValidAudience = "https://localhost",
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
//        ValidateIssuerSigningKey = true,
//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero
//    };
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
