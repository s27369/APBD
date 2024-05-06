using ProbnyKolos.Repos;

namespace ProbnyKolos.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _repo;

    public PrescriptionService(IPrescriptionRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Prescription> GetAllPrescriptions()
    {
        return _repo.GetAllPrescriptions();
    }

    public bool DeletePrescription(int id)
    {
        return _repo.DeletePrescription(id);
    }

    public bool ModifyPrescription(Prescription p)
    {
        return _repo.ModifyPrescription(p);
    }

    public Prescription GetPrescriptionById(int id)
    {
        return _repo.GetPrescriptionById(id);
    }
}