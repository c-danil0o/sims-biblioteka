﻿namespace CirkulacijaBiblioteke.Models;

public class UserAccount
{
    public string Email { get; set; }
    public string Password { get; set; }
    public AccountType Type { get; set; }
    
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