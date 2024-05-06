namespace ProbnyKolos.Services;

public interface IPrescriptionService
{
    public IEnumerable<Prescription> GetAllPrescriptions();
    public bool DeletePrescription(int id);
    public bool ModifyPrescription(Prescription p);
    public Prescription GetPrescriptionById(int id);
}