namespace Zadanie3.Exceptions
{
    public class OverfillException : SystemException
    {
        public OverfillException(string message) : base(message)
        {
        }
    }
}