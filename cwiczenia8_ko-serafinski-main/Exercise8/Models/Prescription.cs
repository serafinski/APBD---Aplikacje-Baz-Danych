namespace Exercise8.Models;

public class Prescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
    public int IdPatient { get; set; }
    //Dajemy znać programowi, że tu będzie jakaś wartość i ona nigdy nie będzie nullem!
    //Obiekty! - wirtualne połączenie
    public virtual Doctor Doctor { get; set; } = null!;
    public virtual Patient Patient { get; set; } = null!;
    //Lista! - wirtualne połączenie
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;
}