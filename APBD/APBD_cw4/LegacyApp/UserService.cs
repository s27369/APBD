using System;

namespace LegacyApp
{
    public class UserService
    {
        // added IsValidUser(), GetAge(), CreateUser(), SetCreditLimit()
        private bool IsValidUser(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            return !string.IsNullOrEmpty(firstName)
                   && !string.IsNullOrEmpty(lastName)
                   && email.Contains("@")
                   && email.Contains(".");
        }
        private int GetAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }
            return age;
        }
        private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            user.HasCreditLimit = client.Type != "VeryImportantClient";

            return user;
        }
        private void SetCreditLimit(User user)
        {
            using (var userCreditService = new UserCreditService())
            {
                var creditLimit = userCreditService.GetCreditLimit(user.LastName);
                if (user.Client.Type == "ImportantClient")
                    creditLimit *= 2;
                user.CreditLimit = creditLimit;
            }
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidUser(firstName, lastName, email, dateOfBirth)) return false;

            int age = GetAge(dateOfBirth);
            if (age < 21) return false;

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = CreateUser(firstName, lastName, email, dateOfBirth, client);

            SetCreditLimit(user);

            if (user.HasCreditLimit && user.CreditLimit < 500) return false;

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
