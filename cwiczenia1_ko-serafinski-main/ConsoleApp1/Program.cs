using System.Text.RegularExpressions;

//https://pja.edu.pl/

//W sytuacji gdy argument nie został przekazany, powinien zostać zwrócony błąd ArgumentNullException

/* It's checking if the args is null or the length of the args is 0. If it is, it's throwing an ArgumentNullException. */
if(args == null || args.Length == 0)
{
    /* It's checking if the args is null or the length of the args is 0. If it is, it's throwing an ArgumentNullException. */
    throw new ArgumentNullException(nameof(args),"Value cannot be null.");
}
//Program otrzymuje pojedynczy argument, który jest adresem URL strony, która będzie celem skanu "crawlera" -> jako program arguments
var url = args[0];

/* Checking if the url is valid. */
if ((Uri.TryCreate(url, UriKind.Absolute, out var result)) &&
    (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
{
    /* Creating a new instance of the HttpClient class. */
    var httpClient = new HttpClient();
    
    /* Creating a new instance of the HashSet class. */
    var set = new HashSet<string>();
    
    /* Sending a GET request to the url and waiting for the response. */
    var httpResult = await httpClient.GetAsync(url);

    
    /* It's checking if the HTTP response status code is in the range of 200-299. */
    if ((int)httpResult.StatusCode >= 200 && (int)httpResult.StatusCode < 300)
    {
        /* A regular expression that matches email addresses. */
        var regex = new Regex(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+");
        
        /* Reading the content of the HTTP response as a string. */
        var httpContent = await httpResult.Content.ReadAsStringAsync();
        
        /* Creating a collection of all matches found in the string. */
        var matches = regex.Matches(httpContent);
        
        
        if (!regex.IsMatch(httpContent))
        {
            //W sytuacji, gdy nie znaleziono żadnych adresów email, powinien zostać zwrócony błąd Exception z informacją Nie znaleziono adresów email
            throw new Exception("Nie znaleziono adresów email");
        }

        //Przeszukuje zawartość strony i wypisuje na konsoli wszystkie adresy email, które zostały znalezione na stronie
        //OPCJA 1
        /* It's adding all the matches to the HashSet. */
        foreach (Match match in matches)
        {
            set.Add(match.Value);
        }

        //Gdy zostały znalezione adresy email, powinny zostać wyświetlone na konsoli. Aplikacja powinna zwracać tylko unikalne adresy email
        /* It's printing all the values from the HashSet. */
        foreach (var value in set)
        {
            Console.WriteLine(value);
        }
        //OPCJA 2
        //matches.Select(match => match.Value).Distinct().ToList().ForEach(e => Console.WriteLine(e));
    }
    else
    {
        //W przypadku, gdy podczas pobierania strony wystąpi błąd (czyli status żądania, który nie jest z przedziału 200-299), powinien zostać zwrócony błąd Exception z informacją Błąd w czasie pobierania strony
        throw new Exception("Błąd w czasie pobierania strony");
    }
}
else
{ 
    //Jeśli został przekazany argument, który nie jest poprawnym adresem URL, powinien zostać zwrócony błąd ArgumentException
    throw new ArgumentException("Niepoprawny url");
}