/*
Modele reprezentują dane apikacji. Typowy model pobiera i przechowuje stan modelu w bazie danych. 
*/
namespace Exercise10.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}