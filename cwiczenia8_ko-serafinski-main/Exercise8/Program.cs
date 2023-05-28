using Exercise8.Models;
using Exercise8.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Dodanie DoctorServices
builder.Services.AddScoped<IDoctorServices, DoctorServices>();

//Dodanie PrescriptionService
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

//Schowanie connection String'a 
builder.Services.AddDbContext<MyDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
