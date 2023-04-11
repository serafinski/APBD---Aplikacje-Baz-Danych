using Exercise3.Repositories;
using Exercise3.Services;

/* Tworzenie obiektu buildera, który będzie wykorzystywany do budowy aplikacji. */
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* Dodanie kontrolerów do aplikacji. */
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

/* Dodanie eksploratora API do aplikacji. */
builder.Services.AddEndpointsApiExplorer();

/* Dodanie generatora Swagger'a do aplikacji. */
builder.Services.AddSwaggerGen();

/* Dodanie do aplikacji usługi `FileDbService`. */
builder.Services.AddSingleton<IFileDbService, FileDbService>();

/* Dodanie do aplikacji `StudentsRepository`. */
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();

/* Budowanie aplikacji. */
var app = builder.Build();

// Configure the HTTP request pipeline.

/* Sprawdzenie, czy aplikacja jest w trybie deweloperskim. Jeśli jest, to użyje generatora Swagger i Swagger UI. */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/* Używane do autoryzacji użytkownika. */
app.UseAuthorization();

/* Używane do mapowania kontrolerów do aplikacji. */
app.MapControllers();

/* Używane do uruchomienia aplikacji. */
app.Run();