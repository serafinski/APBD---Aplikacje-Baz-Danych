using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise10.Models;

public class Movie
{
    //klucz główny
    public int Id { get; set; }
    //Dodawanie walidacji!
    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string? Title { get; set; }
    
    //zmiana by wyświetlał normalnie tekst a nie połączony jak nazwa zmiennej
    [Display(Name = "Release Date")]
    //nie trzeba wprowadzać godziny, wyświetlana będzie tylko sama data
    [DataType(DataType.Date)]
    //Alternatywa (ale lepiej używać DataType): [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime ReleaseDate { get; set; }
    
    //Dodawanie walidacji!
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required]
    [StringLength(30)]
    //nullowalne
    public string? Genre { get; set; }
    
    //Dodawanie walidacji!
    [Range(1, 100)]
    [DataType(DataType.Currency)]
    //mapowanie potrzebne by można było odpowiednio zmapować cenę do waluty
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    //Dodawanie walidacji!
    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [StringLength(5)]
    //Alternatywnie walidacja może być w jednej linijce a nie 2: [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(5)]
    [Required]
    public string? Rating {  get; set; }
}
