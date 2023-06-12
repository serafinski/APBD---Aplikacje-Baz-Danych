using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Exercise10.Data;
using Exercise10.Models;
var builder = WebApplication.CreateBuilder(args);

//Tutaj mamy Dependency injection
builder.Services.AddDbContext<Exercise10Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Exercise10Context") ?? throw new InvalidOperationException("Connection string 'Exercise10Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Seed'uj przy każdym uruchomieniu programu?
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Default'owa logika routowania używana przez MCV
app.MapControllerRoute(
    name: "default",
    //Ten pattern pozwala na ustalenie jaki kod ma zostać wezwany!
    //[Controller] Pierwszy segment określa klasę kontrolera która ma zostać uruchomiona
    //[ActionName] Druga część determinuje akcje metody klasy
    //[Parameters] Trzecia część to id dla routowanych danych -> znak ?, że jest opcjonalny
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();