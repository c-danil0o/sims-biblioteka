using System;
using Newtonsoft.Json;

namespace CirkulacijaBiblioteke.Models;

public class Member
{
    public string JMBG { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string Phone { get; set; }
    public UserAccount? Account { get; set; }
    public Address UserAddress { get; set; }
    public int CardNumber { get; set; }

    [JsonConstructor]
    public Member(string jmbg, string? name, string? lastName, string phone, UserAccount? account, Address userAddress, int cardNumber)
    {
        JMBG = jmbg;
        Name = name;
        LastName = lastName;
        Phone = phone;
        Account = account;
        UserAddress = userAddress;
        CardNumber = cardNumber;
    }

    public Member(string jmbg, string? name, string? lastName, string phone, UserAccount? account, Address userAddress)
    {
        JMBG = jmbg;
        Name = name;
        LastName = lastName;
        Phone = phone;
        Account = account;
        UserAddress = userAddress;
        CardNumber = -1;
    }
}