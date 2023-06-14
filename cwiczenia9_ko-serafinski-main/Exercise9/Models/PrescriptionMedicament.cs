namespace Exercise8.Models;

public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    //? bo może być null'owalna 
    public int? Dose { get; set; }
    //Dajemy znać programowi, że tu będzie jakaś wartość i ona nigdy nie będzie nullem!
    public string Details { get; set; } = null!;
    //Obiekty! - wirtualne połączenie
    public Medicament Medicament { get; set; } = null!;
    public Prescription Prescription { get; set; } = null!;
}