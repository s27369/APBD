using Microsoft.Data.SqlClient;

namespace APBD_cw6;

public class AnimalRepository: IAnimalRepository
{
    private IConfiguration _configuration;
    private string dbname = "Animal";

    public AnimalRepository(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public List<AnimalDTO.GetAnimalsResponse> GetAnimalsResponses(string orderBy)
    {
        
        var animals = new List<AnimalDTO.GetAnimalsResponse>();

        using (var sqlConn = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            // Console.WriteLine("elo");
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

        return animals;
    }

    public AnimalDTO.GetOneAnimalResponse GetAnimal(int id)
    {
        AnimalDTO.GetOneAnimalResponse animal = null;
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            string query = $"SELECT * FROM Animal WHERE IdAnimal={id}";

            var selectAllQuery = new SqlCommand(query, sqlConnection);
            selectAllQuery.Connection.Open();
            var SelectReader = selectAllQuery.ExecuteReader();
            if (!SelectReader.Read()) return null;
            animal = new AnimalDTO.GetOneAnimalResponse(
                SelectReader.GetInt32(0), SelectReader.GetString(1), SelectReader.IsDBNull(2) ? null : SelectReader.GetString(2), SelectReader.GetString(3), SelectReader.GetString(4));

            SelectReader.Close();
            selectAllQuery.Connection.Close();
        }

        return animal;
    }

    public void CreateAnimal(AnimalDTO.CreateAnimalRequest animal)
    {
        string connectionString = _configuration.GetConnectionString("Default");
        if (String.IsNullOrEmpty(connectionString))
        {
            return;
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
    }
}