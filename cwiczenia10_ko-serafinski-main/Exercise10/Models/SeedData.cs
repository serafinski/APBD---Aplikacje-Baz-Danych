using Exercise10.Data;
using Microsoft.EntityFrameworkCore;

namespace Exercise10.Models;

//Ten kod ma Seed'ować bazę danych - robi to kiedy nie widzi żadnych danych w tablicy.
//Robiąc update do bazy danych (np. dodając nowy atrybut) patrz na komentarz pod spodem!!!
public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new Exercise10Context(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<Exercise10Context>>()))
        {
            //UWAGA NA TO! -  nie chciało zupdateować Rating'u bo dane filmów już istniały wcześniej w tabeli.
            //Trzeba usunąć wszystkie dane z tablicy po takiej zmianie!
            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }
            
            context.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Rating = "R",
                    Price = 7.99M
                },
                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Rating = "PG",
                    Price = 8.99M
                },
                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Rating = "PG",
                    Price = 9.99M
                },
                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Rating = "PG",
                    Price = 3.99M
                }
            );
            context.SaveChanges();
        }
    }
}