namespace Exercise8.Models;

public class Medicament
{
    public int IdMedicament { get; set; }
    //Dajemy znać programowi, że tu będzie jakaś wartość i ona nigdy nie będzie nullem!
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Type { get; set; } = null!;
    //Lista! - wirtualne połączenie
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments{ get; set; } = null!;
}