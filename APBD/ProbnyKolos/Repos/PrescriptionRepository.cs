using System.Data.SqlClient;

namespace ProbnyKolos.Repos;

public class PrescriptionRepository : IPrescriptionRepository
{
    private IConfiguration _config;

    public PrescriptionRepository(IConfiguration config)
    {
        _config = config;
    }
    public IEnumerable<Prescription> GetAllPrescriptions()
    {
        using (var conn = new SqlConnection(_config["Default"]))
        {
            conn.Open();
            string db = "Test1";
            string comm = "Select * from @dbname;";
            var getCommand = new SqlCommand();
            getCommand.Connection = conn;
            getCommand.CommandText = comm;
            getCommand.Parameters.AddWithValue("@dbname", db);

            var reader = getCommand.ExecuteReader();
            var l = new List<Prescription>();
            while (reader.Read())
            {
                var p = new Prescription()
                {
                    _date = (DateTime)reader["date"],
                    _doctorLastName = (string)reader["DoctorLastName"]
                };
                l.Add(p);
            }
            conn.Close();
            return l;
        };
    }

    public bool DeletePrescription(int id)
    {
        using var conn = new SqlConnection(_config["Default"]);
        conn.Open();
        using var comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandText = "Delete from Test WHERE id=@id";
        comm.Parameters.AddWithValue("@id", id);
        var val = comm.ExecuteNonQuery();
        conn.Close();
        if (val != 0) return true;
        return false;
    }

    public bool ModifyPrescription(Prescription p)
    {
        using var conn = new SqlConnection(_config["Default"]);
        conn.Open();
        using var comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandText = "UPDATE Test1 Set val1 = @val1 where id = @id";
        comm.Parameters.AddWithValue("@val1", p._date);
        comm.Parameters.AddWithValue("@id", p.idPrescirption);
        var num = comm.ExecuteNonQuery();
        conn.Close();
        if (num != 0) return true;
        return false;

    }

    public Prescription GetPrescriptionById(int id)
    {
        throw new NotImplementedException();
    }
}