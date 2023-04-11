// See https://aka.ms/new-console-template for more information

/*
    PRZYKŁADOWY CONFIG
    D:\PJATK\Semestr_4\APBD\cwiczenia2_ko-serafinski\Studenci\dane.csv
    D:\PJATK\Semestr_4\tmp\
    D:\PJATK\Semestr_4\tmp\logi\logs.txt
    json
 */

using Studenci.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

//Gdy jest przekazana za mała lub za duża ilość argumentów
if (args.Length!=4)
{
    throw new ArgumentOutOfRangeException("args","Specified argument was out of the range of valid values.");
}

//PROGRAM OTRZYMUJE 4 ARGUMENTY
//Ścieżka do pliku z danymi
var csvPath = args[0];
//Ścieżka do folderu, gdzie zostanie wyeksportowany plik wynikowy
var output = args[1];
//Ścieżka do pliku z logami
var logssciezka = args[2];
//Format danych w jakich plik ma zostać wyeksportowany
var format = args[3];

//Gdy zostanie podana ścieżka do pliku z danymi (argument nr 1), który nie istnieje
if (!File.Exists(csvPath))
{
    throw new FileNotFoundException("Unable to find the specified file.");
}

//Gdy zostanie podana ścieżka do folderu wynikowego, który nie istnieje
if (!Directory.Exists(output))
{
    throw new DirectoryNotFoundException("Attempted to access a path that is not on the disk.");
}

//Gdy zostanie podana ścieżka do pliku z logami, który nie istnieje
if (!File.Exists(logssciezka))
{
    throw new FileNotFoundException("Unable to find the specified file.");
}

if (format != "json" && format != "xml" && format != "yaml")
{
    throw new InvalidOperationException("Operation is not valid due to the current state of the object.");
}


/* It's reading the file line by line. */
var csvContent = File.ReadLines(csvPath);

/* It's creating a new list of students. */
var students = new List<Student>();

//Błędy podczas przetwarzania danych powinny być zapisywane do pliku. Nie muszą być wyświetlane na konsoli
var logs = File.AppendText(logssciezka);

/* It's flushing the logs to the file. */
logs.AutoFlush = true;

/* It's creating a new dictionary. */
var dict = new Dictionary<string, int>();

csvContent.ToList().ForEach(line =>
{
    /* It's splitting the line by commas. */
    var splitted = line.Split(',');
    
    //Tych studentów, którzy nie są opisywani przez 9 kolumn z danymi pomijamy. Informacje o pominiętym studencie traktujemy jako błąd i logujemy do pliku.
    if (splitted.Length != 9) {
        Console.WriteLine(line);
        logs.WriteLine($"Wiersz nie posiada odpowiedniej ilości kolumn: {line}");
        return;
    }
    
    //Jeśli jeden wiersz z danymi posiada w kolumnie pustą wartość - traktujemy taką wartość jako brakującą.
    //W takim wypadku nie dodajemy studenta do zbioru wynikowego i zapisujemy następującą do pliku logów
    if(splitted.Any(e => e.Trim() == ""))
    {
        logs.WriteLine($"Wiersz nie może posiadać pustych kolumn: {line}");
        return;
    }

    /* It's creating a new object of type `Studies` and assigning values to its properties. */
    var studies = new Studies
    {
        //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: kierunek
        Name = splitted[2],
        //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: tryb
        Mode = splitted[3],
    };

    /* It's creating a new object of type `Student` and assigning values to its properties. */
    var student = new Student
    {
        //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: imię
        Fname = splitted[0],
        //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: nazwisko
        Lname = splitted[1],
        //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: nrindeksu
        IndexNumber = "s" + splitted[4],
        //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: data
        Birthdate = DateTime.Parse(splitted[5]),
        //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: email
        Email = splitted[6],
        //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: ImieMatki
        MothersName = splitted[7],
        //KOLUMNY PRZEZ KTÓRE OPISYWANY JEST STUDENT TO: ImieOjca
        FathersName = splitted[8],
        //(z obiektu wyżej: kierunek i tryb)
        Studies = studies,
    };
    
    //Musimy zadbać o to, aby nie dodawać do wyniku dwa razy studenta o tym samym imieniu, nazwisku i numerze indeksu.
    //Każde powtórzenie danych o studencie w danych źródłowych traktujemy jako niepoprawny duplikat.
    //Znalezione duplikaty logujemy do pliku.
    if (students.Any(e => e.Fname == student.Fname && e.Lname == student.Lname && e.IndexNumber == student.IndexNumber))
    {
        logs.WriteLine($"Duplikat: {line}");
        return;
    }

    //Chcemy dodatkowo zapisać informację o ilości studentów na danym kierunku. Zbieramy informację o danych kierunkach, które wystąpiły w pliku!
    dict[studies.Name] = dict.ContainsKey(studies.Name) 
    ? ++dict[studies.Name]
    : 1;

    /* It's adding a new student to the list of students. */
    students.Add(student);
});

/* It's serializing the data to JSON. */
var json = JsonSerializer.Serialize(


    
    /* It's creating a new object of type `UczelniaWrapper` and assigning values to its properties. */
    new UczelniaWrapper
    {
        //Przetworzone dane, przygotowujemy do zapisu, gdzie wymagana jest specyficzna hierarchia.
        //Zapisywany będzie obiekt uczelnia, który ma zawierać:
        Uczelnia = new Uczelnia
        {
            //pole createdAt - data wygenerowania pliku, format DD.MM.YYYY
            CreatedAt = DateTime.Now,
            //pole author - imię i nazwisko autora tego kodu
            Author = "Tomasz Serafiński",
            //tablicę students - tablica obiektów Student, po wstępnym przetworzeniu
            Students = students,
            //Do pliku wynikowego chcemy dodać tablicę activeStudies, która przechowuje obiekty: {"name": "nazwaKierunku", "numberOfStudents": liczbaStudentowNaKierunku }
            ActiveStudies = dict.Select(e => 
                new ActiveStudies { Name = e.Key, NumberOfStudents = e.Value }
            )
        }
    },
    
    /* It's creating a new object of type `JsonSerializerOptions` and assigning values to its properties. */
    new JsonSerializerOptions { 
        /* It's writing the data in a human-readable format. */
        WriteIndented = true,
        /* It's encoding the data to a format that can be read by a web browser. */
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        /* It's converting the property names to camel case. */
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    }
);

//Po przygotowaniu danych zapisujemy je do folderu wskazanego przez użytkownika.
//Plik powinien mieć nazwę uczelnia i powien być zapisany ze wskazanym rozszerzeniem np. uczelnia.json
File.WriteAllText(output+"uczelnia."+format, json);
