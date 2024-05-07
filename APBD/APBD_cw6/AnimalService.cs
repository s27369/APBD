namespace APBD_cw6;

public class AnimalService : IAnimalService
{
    private IAnimalRepository _repository;

    public AnimalService(IAnimalRepository repository)
    {
        _repository = repository;
    }

    public List<AnimalDTO.GetAnimalsResponse> GetAnimalsResponses(string orderBy)
    {
        return _repository.GetAnimalsResponses(orderBy);
    }

    public AnimalDTO.GetOneAnimalResponse GetAnimal(int id)
    {
        return _repository.GetAnimal(id);
    }

    public void CreateAnimal(AnimalDTO.CreateAnimalRequest animal)
    {
        _repository.CreateAnimal(animal);
    }
}