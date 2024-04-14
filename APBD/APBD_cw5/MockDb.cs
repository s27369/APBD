namespace APBD_cw5;


public interface IMockDb
{
    public ICollection<Animal> GetAll();
    public Animal? GetById(int id);
    public void Add(Animal animal);
    public bool Delete(int id);
    public bool Update(Animal animal);
}

public class MockDb : IMockDb
{
    private ICollection<Animal> _animals;

    public MockDb()
    {
        _animals = new List<Animal>
        {
            new Animal()
            {
                Id = 1,
                Name = "Irys",
                Category = "rabbit",
                FurColor = "gray",
                Mass = 1.4
            },
            new Animal()
            {
                Id = 2,
                Name = "Figa",
                Category = "rabbit",
                FurColor = "brown",
                Mass = 1.3
            },
            new Animal()
            {
                Id = 3,
                Name = "Peanut",
                Category = "dog",
                FurColor = "beige",
                Mass = 4.8
            }
        };
    }

    public ICollection<Animal> GetAll()
    {
        return _animals;
    }

    public Animal? GetById(int id)
    {
        return _animals.FirstOrDefault(animal => animal.Id == id);
    }

    public void Add(Animal animal)
    {
        _animals.Add(animal);
    }

    public bool Delete(Animal animal)
    {
        return _animals.Remove(animal);
    }

    public bool Delete(int id)
    {
        var animal = GetById(id);
        if (animal == null)
        {
            return false;
        }

        return Delete(animal);

    }

    public bool Update(Animal animal)
    {
        var existingAnimal = _animals.FirstOrDefault(a => a.Id == animal.Id);
        if (existingAnimal != null)
        {
            existingAnimal.Name = animal.Name;
            existingAnimal.Category = animal.Category;
            existingAnimal.Mass = animal.Mass;
            existingAnimal.FurColor = animal.FurColor;
            return true;
        }
        else
            return false;

    }
}