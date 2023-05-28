namespace Exercise8.Models;

public class Patient
{
    public int IdPatient { get; set; }
    //Dajemy znać programowi, że tu będzie jakaś wartość i ona nigdy nie będzie nullem!
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    //Lista! - wirtualne połączenie
    public virtual ICollection<Prescription> Prescriptions { get; set; } = null!;
}