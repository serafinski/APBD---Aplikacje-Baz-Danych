namespace Exercise8.Models.DTOs;

public class PrescriptionMedicamentDto
{
    public int? Dose { get; set; }
    public string Details { get; set; }
    public MedicamentDto Medicament { get; set; }
}