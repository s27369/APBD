namespace APBD_cw6;

public interface IAnimalRepository
{
    public List<AnimalDTO.GetAnimalsResponse> GetAnimalsResponses(string orderBy);
    public AnimalDTO.GetOneAnimalResponse GetAnimal(int id);
    public void CreateAnimal(AnimalDTO.CreateAnimalRequest animal);
}