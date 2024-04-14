namespace Api;

public class Student
{
    public int Id { get; set; }
    public string FName { get; set; }
}

public interface IMockDb
{
    public ICollection<Student> GetAll();
    public Student? GetById(int id);
    public void Add(Student student);
}

public class MockDb : IMockDb
{
    private ICollection<Student> _students;

    public MockDb()
    {
        _students = new List<Student>
        {
            new Student()
            {
                Id = 1,
                FName = "Kacper"
            },
            new Student()
            {
                Id = 2,
                FName = "Krzysztof"
            }
        };
    }

    public ICollection<Student> GetAll()
    {
        return _students;
    }

    public Student? GetById(int id)
    {
        return _students.FirstOrDefault(student => student.Id == id);
    }

    public void Add(Student student)
    {
        _students.Add(student);
    }
}