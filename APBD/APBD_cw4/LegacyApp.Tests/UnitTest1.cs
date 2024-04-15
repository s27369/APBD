namespace LegacyApp.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        UserService userService = new UserService();
        // userService.AddUser("A", "B", "C", new DateTime(new DateOnly(1, 1, 1), new TimeOnly(1, 1), new DateTimeKind()), 1);
        // userService.AddUser("", "B", "C", new DateTime(1, 1, 1), 1);
        // userService.AddUser("A", "", "C", new DateTime(1, 1, 1), 1);
        userService.AddUser("John", "Doe", "Cgmail.com", new DateTime(1, 1, 1), 1);//bad email
        userService.AddUser("John", "Doe", "C@gmailcom", new DateTime(1, 1, 1), 1);//bad email
        userService.AddUser("", "Doe", "C@gmail.com", new DateTime(1, 1, 1), 1);//bad name
        userService.AddUser("John", "Doe", "C@gmail.com", new DateTime(2019, 4,10), 1);//too young
        userService.AddUser("John", "Doe", "C@gmail.com", new DateTime(1919, 2,10), 2);//correct
        // userService.AddUser("A", "", "C", new DateTime(1, 1, 1), 1);
        User u = userService.("John", "Doe", "C@gmail.com", new DateTime(1919, 2,10), 100)
        
    }
}