namespace Zadanie3.Exceptions
{

    public class IncorrectCargoMass : SystemException
    {
        public IncorrectCargoMass(string? message) : base(message)
        {
        }
    }
}