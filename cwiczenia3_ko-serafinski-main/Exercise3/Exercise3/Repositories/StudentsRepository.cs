using Exercise3.Models;
using Exercise3.Services;


namespace Exercise3.Repositories
{
    /* Klasa implementująca interfejs IStudentsRepository. */
    public interface IStudentsRepository
    {
        /* Funkcja zwracająca listę studentów */
        IEnumerable<Student> GetStudents();
        
        /*Funkcja mająca za zadanie usunąć studenta z repozytorium,
         bool bo zwraca informacje czy usunięcie zakończyło się sukcesem. */
        Task<bool> DeleteStudent (Student student);
        
        
        /* Funkcja dodająca studenta do repozytorium,
         zagnieżdżony task - bo służy do śledzenia postępu operacji dodawania. */
        Task<Task<Task>> AddStudent(Student student);
        
        /* Funkcja aktualizująca dane studenta w repozytorium nowymi danymi. */
        Task<Task> UpdateStudent(Student student, Student newData);
    }
    
    public class StudentsRepository : IStudentsRepository
    {
        
        private readonly IFileDbService _fileDbService;
        
        public StudentsRepository(IFileDbService fileDbService)
        {
            _fileDbService = fileDbService;
        }
        
        /*Zwracanie kolekcji obiektów Student*/
        public IEnumerable<Student> GetStudents()
        {
            return _fileDbService.Students;
        }

        /*Funkcja mająca za zadanie usunąć studenta z repozytorium,
        bool bo zwraca informacje czy usunięcie zakończyło się sukcesem. */
        public async Task<bool> DeleteStudent(Student student)
        {
            /* Próba usunięcia studenta z kolekcji */
            var result = ((List<Student>)_fileDbService.Students).Remove(student);
            
            /*Jeżeli się powiedzie... */
            if (result)
            {
                /*... zapisanie zmian w bazie danych. */
                await _fileDbService.SaveChanges();
            }

            return result;
        }

        /*Metoda zwracająca zagnieżdżone zadanie*/
        public async Task<Task<Task>> AddStudent(Student student)
        {
            /* Dodaje określonego obiektu "Student" do kolekcji studentów przechowywanych w plikowej bazie danych.  */
            ((List<Student>)_fileDbService.Students).Add(student);
            
            /* Zapisywanie wprowadzonych zmian w plikowej bazie danych. */
            await _fileDbService.SaveChanges();
            
            /* Zwrócenie zagnieżdżonego zadania w celu poinformowania użytkownika, że operacja została zakończona. */
            return Task.FromResult(Task.CompletedTask);
        }

        
        /* Aktualizacja określonego obiektu "Student" o nowe dane przekazane w parametrze "newData".*/
        public async Task<Task> UpdateStudent(Student student, Student newData)
        {
            student.FirstName = newData.FirstName;
            student.LastName = newData.LastName;
            student.IndexNumber = newData.IndexNumber;
            student.BirthDate = newData.BirthDate;
            student.StudyName = newData.StudyName;
            student.StudyMode = newData.StudyMode;
            student.Email = newData.Email;
            student.FathersName = newData.FathersName;
            student.MothersName = newData.MothersName;

            /* Zapisanie zmian w bazie danych. */
            await _fileDbService.SaveChanges();
            
            /* Zwracanie zakończonego zadania. */
            return Task.CompletedTask;
        }
    }
}
