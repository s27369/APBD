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
    private string dbname = "Animal";
    private IConfiguration _configuration;
    private readonly IValidator<AnimalDTO.CreateAnimalRequest> _createAnimalValidator;
    private readonly IValidator<AnimalDTO.EditAnimalRequest>_editAnimalValidator;

    public AnimalControllers(IConfiguration configuration, IValidator<AnimalDTO.CreateAnimalRequest> createAnimalValidator, IValidator<AnimalDTO.EditAnimalRequest> editAnimalValidator)
    {
        this._configuration = configuration;
        _createAnimalValidator = createAnimalValidator;
        _editAnimalValidator = editAnimalValidator;
    }
    [HttpGet]
    public IActionResult GetAnimals(string orderBy = null)
    {
        var animals = new List<AnimalDTO.GetAnimalsResponse>();

        using (var sqlConn = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            Console.WriteLine("elo");
            string q = "SELECT * FROM " + dbname;

            if (string.IsNullOrEmpty(orderBy)) q += " ORDER BY name ASC";
            else
            {
                var colNamesCommand =
                    new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='Animal'",
                        sqlConn);
                colNamesCommand.Connection.Open();
                var colNamesReader = colNamesCommand.ExecuteReader();
                while (colNamesReader.Read())
                {
                    if (orderBy.ToLower() == colNamesReader.GetString(0).ToLower()) q += $" ORDER BY {orderBy} ASC";
                }
                colNamesReader.Close();
                colNamesCommand.Connection.Close();
            }
            
            var selectAllCommand = new SqlCommand(q, sqlConn);
            selectAllCommand.Connection.Open();
            var selectAllReader = selectAllCommand.ExecuteReader();
            while (selectAllReader.Read())
            {
                animals.Add(new AnimalDTO.GetAnimalsResponse(
                    selectAllReader.GetInt32(0),
                    selectAllReader.GetString(1),
                    selectAllReader.IsDBNull(2) ? null : selectAllReader.GetString(2),
                    selectAllReader.GetString(3),
                    selectAllReader.GetString(4))
                );
            }
            selectAllReader.Close();
            selectAllCommand.Connection.Close();
        }
        return Ok();
    }
    
    [HttpGet("{id}")]
    public IActionResult GetAnimal(int id)
    {
    	AnimalDTO.GetOneAnimalResponse animal = null;
    	using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
    	{
    		string query = $"SELECT * FROM Animal WHERE IdAnimal={id}";

    		var selectAllQuery = new SqlCommand(query, sqlConnection);
    		selectAllQuery.Connection.Open();
    		var SelectReader = selectAllQuery.ExecuteReader();
    		if (!SelectReader.Read()) return NotFound("Animal with this index does not exist!");
    		animal = new AnimalDTO.GetOneAnimalResponse(
    		SelectReader.GetInt32(0), SelectReader.GetString(1), SelectReader.IsDBNull(2) ? null : SelectReader.GetString(2), SelectReader.GetString(3), SelectReader.GetString(4));

    		SelectReader.Close();
    		selectAllQuery.Connection.Close();
    	}
    	return Ok(animal);
    }
    
    [HttpPost]
		public IActionResult CreateAnimal(string json)
		{
			if (string.IsNullOrEmpty(json)) return BadRequest("You must provide data");
			if (!CheckIsJson(json)) return BadRequest("Data must be formatted as JSON");

			AnimalDTO.CreateAnimalRequest animal;
			try
			{
				animal = JsonConvert.DeserializeObject<AnimalDTO.CreateAnimalRequest>(json);

				if (animal == null) return BadRequest("You must provide data");
				var validation = _createAnimalValidator.Validate(animal);
				if (!validation.IsValid)
				{
					var problemDetails = new ValidationProblemDetails(new ModelStateDictionary());
					foreach (var error in validation.Errors)
					{
						problemDetails.Errors.Add(error.PropertyName, new[] { error.ErrorMessage });
					}
					return ValidationProblem(problemDetails);
				}
			}
			catch (Newtonsoft.Json.JsonException)
			{
				return BadRequest("Pass correct data!");
			}

			string connectionString = _configuration.GetConnectionString("Default");
			if (String.IsNullOrEmpty(connectionString))
			{
				return NotFound("Database connection string is not configured.");
			}

			using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				if (animal.Desc != null)
					using (var command = new SqlCommand(
							"INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Desc, @Cat, @Area);", connection))
					{
						command.Parameters.AddWithValue("@Name", animal.Name);
						command.Parameters.AddWithValue("@Description", animal.Desc);
						command.Parameters.AddWithValue("@Category", animal.Cat);
						command.Parameters.AddWithValue("@Area", animal.Area);

						command.ExecuteNonQuery();
					}
				else
					using (var command = new SqlCommand(
							"INSERT INTO Animal (Name, Category, Area) VALUES (@Name, @Cat, @Area);", connection))
					{
						command.Parameters.AddWithValue("@Name", animal.Name);
						command.Parameters.AddWithValue("@Category", animal.Cat);
						command.Parameters.AddWithValue("@Area", animal.Area);

						command.ExecuteNonQuery();
					}
			}
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