using System.Diagnostics;
using System.Text;
using Exercise8.Extensions;
using Exercise8.Models;
using Exercise8.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

//Dodanie DoctorServices
builder.Services.AddScoped<IDoctorServices, DoctorServices>();

//Dodanie PrescriptionService
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

//Dodanie AccountsService
builder.Services.AddScoped<IAccountsService, AccountService>();


//Schowanie connection String'a 
builder.Services.AddDbContext<MyDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Autentyfikacja
builder.Services.AddAuthentication().AddJwtBearer(opt =>
{
    //Jakie wartości powinny znaleźć się w kluczu by to w ogóle było poprawne!
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        // Walidacja wydawcy - że klucz pochodzi z określonego serwera
        ValidateIssuer = true,
        // Kto to może w ogóle wysłać
        ValidateAudience = true,
        // Sprawdzenie czy token jest ważny
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JWT:Issuer"],
        ValidAudience = config["JWT:Audience"],
        // Nasz secret - dzięki temu klucz wygląda za każdym razem inaczej
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!))
    };
});

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

//Middleware
//context - co wysłał użytkownik a co otrzymał
//next - wykonanie następnej funkcji
app.Use(async (context, next) =>
{
    Debug.WriteLine("Hello world");
    //bez tej linijki zatnie nam się program
    await next(context);
});

//Middleware wydzielony jako metoda rozszerzona
app.ConfigureExceptionHandler();

app.Run();
