namespace Exercise8.Models.DTOs;

public class GetPrescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDto Doctor { get; set; }
    public PatientDto Patient { get; set; }
    public List<PrescriptionMedicamentDto> PrescriptionMedicaments { get; set; }
}