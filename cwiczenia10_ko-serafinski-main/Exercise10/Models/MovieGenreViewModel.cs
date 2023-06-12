using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Exercise10.Models;

//potrzebujemy do wyszukiwania po gatunku filmu
public class MovieGenreViewModel
{
    public List<Movie>? Movies { get; set; }
    //SelectList pozwala użytkownikowi na wybór z listy gatunków
    public SelectList? Genres { get; set; }
    //Wybrany gatunek
    public string? MovieGenre { get; set; }
    //Tekst w search box'ie
    public string? SearchString { get; set; }
}