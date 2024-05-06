namespace LegacyApp.Tests;

public class UnitTest1
{
    [Fact]
    public void AddUser_ValidUser_ReturnsTrue()
    {
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var clientId = 1;

        var result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        Assert.True(result);
    }

    [Fact]
    public void AddUser_InvalidEmail_ReturnsFalse()
    {
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Doe";
        var email = "johndoe"; // invalid email
        var dateOfBirth = new DateTime(1990, 1, 1);
        var clientId = 1;

        var result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        Assert.False(result);
    }

    [Fact]
    public void AddUser_UnderageUser_ReturnsFalse()
    {
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var dateOfBirth = DateTime.Now.AddYears(-18); // underage
        var clientId = 1;

        var result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        Assert.False(result);
    }
    [Fact]
    public void AddUser_UserBornInJanuary_ReturnsTrue()
    {
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1991, 01, 01); 
        var clientId = 1;

        var result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        Assert.True(result);
    }
    
    [Fact]
    public void AddUser_VeryImportantClient_ReturnsTrue()
    {
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Malewski";
        var email = "Malewski@example.com";
        var dateOfBirth = new DateTime(1991, 01, 01); 
        var clientId = 2;

        var result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        Assert.True(result);
    }
    [Fact]
    public void AddUser_ImportantClient_ReturnsTrue()
    {
        var userService = new UserService();
        var firstName = "John";
        var lastName = "Smith";
        var email = "Smith@example.com";
        var dateOfBirth = new DateTime(1991, 01, 01); 
        var clientId = 3;

        var result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        Assert.True(result);
    }
}