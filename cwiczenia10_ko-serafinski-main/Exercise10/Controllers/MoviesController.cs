using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise10.Data;
using Exercise10.Models;

namespace Exercise10.Controllers
{
    public class MoviesController : Controller
    {
        private readonly Exercise10Context _context;
        
        //Dependency injection - tym razem w kontrolerze
        //Ten kontroler robi CRUD - Create, Remove, Update, Delete
        public MoviesController(Exercise10Context context)
        {
            _context = context;
        }

        // GET: Movies
        
        //
        //Zmiana na searchString na id pozwala na użycie adres w tym stylu: https://localhost:7042/movies/index/ghost
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'Exercise10Context.Movie'  is null.");
            }
            
            //Zapytanie LINQ pozwalające na dostanie listy wszystkich gatunków
            IQueryable<string> genreQuery = from m in _context.Movie
                orderby m.Genre
                select m.Genre;
            
            //Zapytanie LINQ pozwalające na wybranie filmów
            var movies = from m in _context.Movie
                select m;
            
            //Filtrowanie na podstawie wprowadzonego string'a
            if (!string.IsNullOrEmpty(searchString))
            {
                //Lambda
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }
            
            //Filtrowanie na podstawie gatunku
            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }
            
            //View Model
            var movieGenreVm = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVm);
            /*return View(await movies.ToListAsync());*/
        }
        
        
        /*public async Task<IActionResult> Index()
        {
              return _context.Movie != null ? 
                        //Widok tworzy listę obiektów jak jest wywoływany
                          View(await _context.Movie.ToListAsync()) :
                          Problem("Entity set 'Exercise10Context.Movie'  is null.");
        }*/

        // GET: Movies/Details/5
        
        //id - używane jako route data - np. https://localhost:7042/movies/details/1
        // można je też przesłać za pomocą https://localhost:7042/movies/details?id=1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }
            //lambda - wybieranie filmu który pasuje do wprowadzonych danych - w tym wypadku id
            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            
            //jak film zostanie znaleziony - zwraca widok
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            //Walidacja modelu!!!
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //Zapobieganiu fałszerstwu żądania - powiązany z tokenem zabezpieczającym przed fałszerstwem generowanym w pliku widoku edycji
        [ValidateAntiForgeryToken]
        //[Bind] - ochrona przed Overposting - w atrybucie umieszczamy tylko te właściwości które chcemy zmienić.
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }
            
            //Sprawdzenie czy dane spełniają wszystkie wymagania by zedytować / zupdate'tować film
            if (ModelState.IsValid)
            {
                try
                {
                    //Zapis do bazy
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //wyświetl Index - włącznie z właśnie zrobionymi zmianami
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        //Ta metoda nie usuwa - ona tylko wyświetla widok!
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        //Ta metoda fizycznie usuwa!
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'Exercise10Context.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
