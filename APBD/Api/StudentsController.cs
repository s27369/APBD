using Microsoft.AspNetCore.Mvc;

namespace Api;

[ApiController]
[Route("cstudents")]
public class StudentsController : ControllerBase
{

    private IMockDb _mockDb;

    public StudentsController(IMockDb mockDb)
    {
        _mockDb = mockDb;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_mockDb.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var student = _mockDb.GetById(id);
        if (student is null) return NotFound();
    
        return Ok(student);
    }

    [HttpPost]
    public IActionResult Add(Student student)
    {
        _mockDb.Add(student);
        return Created();
    }
    
}