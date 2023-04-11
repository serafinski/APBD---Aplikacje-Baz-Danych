using System.Globalization;
using Exercise3.Models;

namespace Exercise3.Services
{
    public interface IFileDbService
    {
        /*Zapewnienie sposobu na uzyskanie i ustawienie wartości zmiennej "Students",
         która reprezentuje kolekcję obiektów "Student".*/
        public IEnumerable<Student> Students { get; set; }
        
        /* Publiczna metoda asynchroniczna, która zwraca Task.
         Służy do zapisania wszystkich zmian dokonanych w kontekście usługi do bazowej bazy danych.*/
        Task SaveChanges();
    }
    
    public class FileDbService : IFileDbService
    {
        /* Zmienna prywatna, która służy do przechowywania ścieżki do pliku. */
        private readonly string _pathToFileDatabase;
        
        /* Właściwość, która służy do uzyskiwania i ustawiania wartości zmiennej Students. */
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        
        
        public FileDbService(IConfiguration configuration)
        {
            /* Pobieranie ścieżki do pliku bazy danych */
            
            /*Jeśli string połączenia "Default" nie zostanie znaleziony lub ma wartość null,
             konstruktor rzuca wyjątek "ArgumentNullException" z nazwą parametru "configuration".*/
            _pathToFileDatabase = configuration.GetConnectionString("Default") ?? throw new ArgumentNullException(nameof(configuration));
            
            /* Metoda wykonywająca wszelkie niezbędne zadania inicjalizacyjne */
            Initialize();
        }

        
        private void Initialize()
        {
            /* Sprawdzenie, czy plik określony przez pole "_pathToFileDatabase" istnieje. */
            if (!File.Exists(_pathToFileDatabase))
            {
                return;
            }
            
            /* Odczytanie pliku, parsowanie danych i zapisanie ich na liście Studentów */
            var lines = File.ReadLines(_pathToFileDatabase);

            /* Tworzy nową listę studentów. */
            var students = new List<Student>();

            lines.ToList().ForEach(line =>
            {
                /* Dzieli linię po przecinkach */
                var splitted = line.Split(',');
    
                //Tych studentów, którzy nie są opisywani przez 9 kolumn z danymi pomijamy.
                if (splitted.Length != 9) {
                    return;
                }
    
                //Jeśli jeden wiersz z danymi posiada w kolumnie pustą wartość - traktujemy taką wartość jako brakującą.
                //W takim wypadku nie dodajemy studenta do zbioru wynikowego.
                if(splitted.Any(e => e.Trim() == ""))
                {
                    return;
                }

                /* Tworzenie nowego obiektu typu `Student` i przypisywanie wartości jego właściwościom. */
                var student = new Student
                {
                    //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: imię
                    FirstName = splitted[0],
                    //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: nazwisko
                    LastName = splitted[1],
                    //Nazwa studiów
                    StudyName = splitted[2],
                    //Nazwa trybu studiów
                    StudyMode = splitted[3],
                    //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: nrindeksu
                    IndexNumber = "s" + splitted[4],
                    //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: data
                    BirthDate = DateTime.Parse(splitted[5]).ToString(CultureInfo.CurrentCulture),
                    //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: email
                    Email = splitted[6],
                    //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: ImieMatki
                    MothersName = splitted[7],
                    //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: ImieOjca
                    FathersName = splitted[8]
                };
    
                //Musimy zadbać o to, aby nie dodawać do wyniku dwa razy studenta o tym samym imieniu, nazwisku i numerze indeksu.
                if (students.Any(e => e.FirstName == student.FirstName && e.LastName == student.LastName && e.IndexNumber == student.IndexNumber))
                {
                    return;
                }

                /* Dodawanie nowego studenta do listy studentów */
                students.Add(student);
            });

            /* Przypisuje wartość zmiennej students do właściwości Students. */
            Students = students;
        }

        /* Zapisywanie zmian wprowadzonych we właściwości "Students" obiektu "FileDbService" do bazy danych w pliku */
        public async Task SaveChanges()
        {
            /* Wywoływane aby zapisać dane do pliku określonego przez pole "_pathToFileDatabase".*/
            await File.WriteAllLinesAsync(
                _pathToFileDatabase, 
                Students.Select(e => $"{e.FirstName}" +
                                     $"{e.LastName}" +
                                     $"{e.IndexNumber}" +
                                     $"{e.BirthDate}" +
                                     $"{e.StudyName}" +
                                     $"{e.StudyMode}" +
                                     $"{e.Email}" +
                                     $"{e.FathersName}" +
                                     $"{e.MothersName}")
            );
        }

    }
}