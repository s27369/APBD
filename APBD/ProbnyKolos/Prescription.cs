namespace ProbnyKolos;

public class Prescription
{
    public int idPrescirption { get; set; }
    public DateTime _date { get; set; }
    public DateTime _dueDate { get; set; }
    public string _patientLastName { get; set; }
    public string _doctorLastName { get; set; }
}