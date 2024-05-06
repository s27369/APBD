using Microsoft.AspNetCore.Mvc;
using ProbnyKolos.Services;

namespace ProbnyKolos.Controllers;
[Route("/api/prescriptions")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    
    private readonly IPrescriptionService _service;

    public PrescriptionController( IPrescriptionService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllPrescriptions()
    {
        return Ok(_service.GetAllPrescriptions());
    }
    [HttpGet("{id:int}")]
    public IActionResult GetPrescriptionById(int id)
    {
        return Ok(_service.GetPrescriptionById(id));
    }

    [HttpDelete]
    public IActionResult DeletePrescription(int id)
    {
        return Ok(_service.DeletePrescription(id));
    }

    [HttpPut]
    public IActionResult ModifyPrescription(Prescription p)
    {
        return Ok(_service.ModifyPrescription(p));
    }
    
}