using Cw7.Models;
using Cw7.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Dodanie Service
builder.Services.AddScoped<ITripsServices, TripsServices>();
builder.Services.AddScoped<IClientServices, ClientServices>();
builder.Services.AddDbContext<MasterContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
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