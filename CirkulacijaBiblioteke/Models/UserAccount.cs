using System.Text.Json.Serialization;

namespace CirkulacijaBiblioteke.Models;

public class UserAccount
{
    public string Email { get; set; }
    public string Password { get; set; }
    public AccountType Type { get; set; }

    [JsonConstructor]
    public UserAccount(string email, string password, AccountType type)
    {
        Email = email;
        Password = password;
        Type = type;
    }
    public bool ValidatePassword(string password)
    {
        return Password == password;
    }

    public enum AccountType
    {
        Member,
        Director,
        Librarian,
        Archivist
    }

}