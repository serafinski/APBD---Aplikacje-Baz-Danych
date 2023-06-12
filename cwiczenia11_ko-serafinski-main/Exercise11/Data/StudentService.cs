namespace Exercise11.Data;

public interface IStudentService
{
    public ICollection<Student> GetStudents();
    public Student? GetStudentById(int id);

    public void RemoveStudentByID(int id);
}

public class StudentService : IStudentService
{
    private ICollection<Student> _students;

    public StudentService()
    {
        _students = new List<Student>
        {
            new Student
            {
                Id = 1,
                AvatarUrl = "https://cdn.pixabay.com/photo/2020/07/01/12/58/icon-5359553_1280.png",
                FirstName = "Jan",
                LastName = "Kowalski",
                Birthdate = DateTime.Now.ToString("d"),
                Studies = "Informatyka"
            },
            new Student
            {
                Id = 2,
                AvatarUrl = "https://cdn.pixabay.com/photo/2020/07/01/12/58/icon-5359553_1280.png",
                FirstName = "Anna",
                LastName = "Malewska",
                Birthdate = DateTime.Now.ToString("d"),
                Studies = "Informatyka"
            },
            new Student
            {
                Id = 3,
                AvatarUrl = "https://cdn.pixabay.com/photo/2020/07/01/12/58/icon-5359553_1280.png",
            },
            new Student
            {
                Id = 4,
                AvatarUrl = "https://cdn.pixabay.com/photo/2020/07/01/12/58/icon-5359553_1280.png",
            },
            new Student
            {
                Id = 5,
                AvatarUrl = "https://cdn.pixabay.com/photo/2020/07/01/12/58/icon-5359553_1280.png",
            },
            new Student
            {
                Id = 6,
                AvatarUrl = "https://cdn.pixabay.com/photo/2020/07/01/12/58/icon-5359553_1280.png",
            },
            new Student
            {
                Id = 7,
                AvatarUrl = "https://cdn.pixabay.com/photo/2020/07/01/12/58/icon-5359553_1280.png",
            },
            new Student
            {
                Id = 8,
                AvatarUrl = "https://cdn.pixabay.com/photo/2020/07/01/12/58/icon-5359553_1280.png",
            },
            new Student
            {
                Id = 9,
                AvatarUrl = "https://cdn.pixabay.com/photo/2020/07/01/12/58/icon-5359553_1280.png",
            },
            new Student
            {
                Id = 10,
                AvatarUrl = "https://cdn.pixabay.com/photo/2020/07/01/12/58/icon-5359553_1280.png",
            }
        };
    }

    /*Zwraca kolekcje studentów*/
    public ICollection<Student> GetStudents()
    {
        return _students;
    }
    
    /*Zwraca studenta po id*/
    public Student? GetStudentById(int id)
    {
        return _students.FirstOrDefault(e => e.Id == id);
    }

    public void RemoveStudentByID(int id)
    {
        var studentToRemove = _students.FirstOrDefault(e => e.Id == id);
        if (studentToRemove != null)
        {
            _students.Remove(studentToRemove);
        }
    }
}