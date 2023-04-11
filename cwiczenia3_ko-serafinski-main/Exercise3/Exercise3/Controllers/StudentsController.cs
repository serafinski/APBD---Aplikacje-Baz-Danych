using Exercise3.Models.DTOs;
using Exercise3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Exercise3.Controllers
{
    /* Atrybut trasy, który mówi kontrolerowi, aby używał nazwy kontrolera jako trasy. */
    [Route("api/[controller]")]
    
    
    [ApiController]
    
    /* Jest to kontroler, który obsługuje żądania do punktu końcowego `/studenci` */
    public class StudentsController : ControllerBase
    {
        /* Jest to pole przechowujące referencję do obiektu `IStudentsRepository`. */
        private readonly IStudentsRepository _studentsRepository;
        
        /* Jest to konstruktor, który przyjmuje `IStudentsRepository` jako parametr i przypisuje go do pola
        `_studentsRepository`. */
        public StudentsController(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        /* Jest to atrybut, który mówi kontrolerowi,
         aby obsługiwał żądania do punktu końcowego `/studenci` za pomocą metody `GET`*/
        [HttpGet]
        
        /* Metoda próbuje pobrać listę studentów z "studentsRepository"
           i zwraca odpowiedź 200 OK z listą studentów zawiniętą w IActionResult.*/
        public Task<IActionResult> Get()
        {
            try
            {
                /*Metoda "Ok" reprezentuje kod statusu HTTP 200, który wskazuje, że żądanie zakończyło się sukcesem. */
                return Task.FromResult<IActionResult>(Ok(_studentsRepository.GetStudents()));
            }
            catch(Exception ex)
            {
                /* Metoda zwraca odpowiedź HTTP 500 Internal Server Error z obiektem ProblemDetails.
                 Wskazuje to, że na serwerze wystąpił nieoczekiwany błąd. */
                return Task.FromResult<IActionResult>(Problem());
            }
        }

        /* Jest to atrybut, który mówi kontrolerowi,
         aby obsługiwał żądania do punktu końcowego `/studenci/{index}` z metodą `GET`. */
        [HttpGet("{index}")]
        
        /*
        Ten kod definiuje metodę o nazwie "Get", która akceptuje string o nazwie "index". 
        
        Metoda zwraca zadanie typu IActionResult, 
        wskazując, że jest to metoda asynchroniczna, która zwraca odpowiedź HTTP. 
        */
        public Task<IActionResult> Get(string index)
        {
            try
            {
                /* Metoda próbuje pobrać studenta z repozytorium na podstawie podanego parametru "index" */
                var student = _studentsRepository.GetStudents().FirstOrDefault(e => e.IndexNumber == index);
                
                return student is null ?
                    /* Jeśli student nie zostanie znaleziony,
                     metoda zwraca odpowiedź HTTP 404 Not Found wskazującą, że żądany zasób nie został znaleziony. */
                    Task.FromResult<IActionResult>(NotFound()) :
                    
                    /* Jeśli student zostanie znaleziony,
                     metoda zwraca odpowiedź HTTP 200 OK z uczniem zawiniętym w obiekt IActionResult. */
                    Task.FromResult<IActionResult>(Ok(student));
            }
            catch(Exception ex)
            {
                /* Metoda zwraca odpowiedź HTTP 500 Internal Server Error z obiektem ProblemDetails.
                 Wskazuje to, że na serwerze wystąpił nieoczekiwany błąd. */
                return Task.FromResult<IActionResult>(Problem());
            }
        }

        /* Jest to atrybut, który mówi kontrolerowi,
         aby obsługiwał żądania do punktu końcowego `/studenci/{index}` z metodą `PUT`. */
        [HttpPut("{index}")]
        
        
        public async Task<IActionResult> Put(string index, StudentPut newStudentData)
        {
            try
            {
                /* Metoda próbuje pobrać studenta z repozytorium na podstawie podanego parametru "index".*/
                var student = _studentsRepository.GetStudents().FirstOrDefault(e => e.IndexNumber == index);
                
                /* Jeżeli student nie zostanie znaleziony... */
                if (student is null)
                {
                    /* ...metoda zwraca odpowiedź HTTP 404 Not Found wskazującą, że żądany zasób nie został znaleziony */
                    return NotFound();
                }
                
                /*Jeżeli student zostanie odnaleziony,
                 metoda aktualizuje dane studenta o nowe dane podane w parametrze "newStudentData"...*/
                await _studentsRepository.UpdateStudent(student, new Models.Student
                {
                    FirstName = newStudentData.FirstName,
                    BirthDate = newStudentData.BirthDate,
                    Email = newStudentData.Email,
                    FathersName = newStudentData.FathersName,
                    IndexNumber = index,
                    LastName = newStudentData.LastName,
                    MothersName = newStudentData.MothersName,
                    StudyName = newStudentData.StudyName,
                    StudyMode = newStudentData.StudyMode
                });
                
                /*... i zwraca odpowiedź HTTP 200 OK z zaktualizowanym obiektem studenta zawiniętym w obiekt IActionResult */
                return Ok(student);
            }
            catch(Exception ex)
            {
                /* Metoda zwraca odpowiedź HTTP 500 Internal Server Error z obiektem ProblemDetails.
                 Wskazuje to, że na serwerze wystąpił nieoczekiwany błąd. */
                return Problem();
            }
        }

        /* Jest to atrybut, który mówi kontrolerowi,
         aby obsługiwał żądania do punktu końcowego `/studenci` za pomocą metody `POST`. */
        [HttpPost]
        
        public async Task<IActionResult> Post(StudentPost newStudent)
        {
            /* Próba pobrania studenta z repozytorium na podstawie właściwości "IndexNumber" parametru "newStudent" */
            var student = _studentsRepository.GetStudents().FirstOrDefault(e => e.IndexNumber == newStudent.IndexNumber);

            /* Jeśli student o tym samym numerze indeksu już istnieje w repozytorium...*/
            if (student is not null)
            {
                /* ... metoda zwraca odpowiedź HTTP 409 Conflict wskazującą,
                 że żądanie jest sprzeczne z bieżącym stanem serwera. */
                return Conflict();
            }
            
            /* Metoda dodaje nowego studenta do repozytorium z danymi podanymi w parametrze "newStudent" */
            await _studentsRepository.AddStudent(new Models.Student
            {
                FirstName = newStudent.FirstName,
                BirthDate = newStudent.BirthDate,
                Email = newStudent.Email,
                FathersName = newStudent.FathersName,
                IndexNumber = newStudent.IndexNumber,
                LastName = newStudent.LastName,
                MothersName = newStudent.MothersName,
                StudyName = newStudent.StudyName,
                StudyMode = newStudent.StudyMode
            });
            
            /* Metoda zwraca odpowiedź HTTP 201 Created informującą, że zasób został pomyślnie utworzony. */
            return Created("/api/students/" + newStudent.IndexNumber, newStudent);
        }

        /* Jest to atrybut, który mówi kontrolerowi,
         aby obsługiwał żądania do punktu końcowego `/studenci/{index}` za pomocą metody `DELETE`. */
        [HttpDelete("{index}")]
        
        
        public async Task<IActionResult> Delete(string index)
        {
            try
            {
                /* Metoda próbuje pobrać studenta z repozytorium na podstawie podanego parametru "index" */
                var student = _studentsRepository.GetStudents().FirstOrDefault(e => e.IndexNumber == index);
                
                /* Jeśli student nie zostanie znaleziony... */
                if (student is null)
                {
                    /* ...metoda zwraca odpowiedź HTTP 404 Not Found wskazującą, że żądany zasób nie został znaleziony. */
                    return NotFound();
                }

                /* Metoda wywołuje funkcję "DeleteStudent" na obiekcie repozytorium, aby usunąć studenta z repozytorium. */
                await _studentsRepository.DeleteStudent(student);
                
                /* Metoda zwraca odpowiedź HTTP 200 OK wskazującą, że zasób został pomyślnie usunięty */
                return Ok(student);
            }
            catch(Exception ex)
            {
                /* Metoda zwraca odpowiedź HTTP 500 Internal Server Error z obiektem ProblemDetails.
                 Wskazuje to, że na serwerze wystąpił nieoczekiwany błąd. */
                return Problem();
            }
        }

    }
}
