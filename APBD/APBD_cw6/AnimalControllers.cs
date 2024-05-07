using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace APBD_cw6;

[ApiController]
[Route("/animals")]
public class AnimalControllers : ControllerBase
{
	private IAnimalService _service;

    public AnimalControllers(IAnimalService service)
    {
	    this._service = _service;
    }
    [HttpGet]
    public IActionResult GetAnimals(string orderBy = null)
    {
        return Ok(_service.GetAnimalsResponses(orderBy));
    }
    
    [HttpGet("{id}")]
    public IActionResult GetAnimal(int id)
    {
    	return Ok(_service.GetAnimal(id));
    }
    
    [HttpPost]
	public IActionResult CreateAnimal(AnimalDTO.CreateAnimalRequest animal)
	{
		_service.CreateAnimal(animal);
		return Ok();
	}
	
	[HttpDelete("{id}")]
	public IActionResult RemoveAnimal(int id)
	{
		AnimalDTO.GetOneAnimalResponse animal = null;
		using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
		{
			sqlConnection.Open();
			string query = $"DELETE FROM Animal WHERE IdAnimal={id}";

			using (var deleteQuery = new SqlCommand(query, sqlConnection))
			{
				int rowsAffected = deleteQuery.ExecuteNonQuery();
				if (rowsAffected == 0) return NotFound("Animal with this index does not exist!");
			}
		}
		return Ok(animal);
	}
	private static bool CheckIsJson(string input)
	{
		try
		{
			var obj = System.Text.Json.JsonSerializer.Deserialize<object>(input);
			return true;
		}
		catch (System.Text.Json.JsonException)
		{
			return false;
		}
	}
		    
}