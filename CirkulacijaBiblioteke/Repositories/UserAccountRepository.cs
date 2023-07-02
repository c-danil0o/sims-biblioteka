using System.Collections.Generic;
using System.Linq;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Utilities;
using Newtonsoft.Json.Linq;

namespace CirkulacijaBiblioteke.Repositories;

public class UserAccountRepository : ISerializable
{
    private readonly string _fileName = @".\..\..\..\Data\accounts.json";
    private List<UserAccount>? _accounts;

    public UserAccountRepository()

    {
        _accounts = new List<UserAccount>();
        Serializer.Load(this);
    }

    public string FileName()
    {
        return _fileName;
    }

    public IEnumerable<object>? GetList()
    {
        return _accounts;
    }

    public void Import(JToken token)
    {
        _accounts = token.ToObject<List<UserAccount>>();
    }


    public IEnumerable<UserAccount> GetAll()
    {
        return _accounts;
    }

    public void Insert(UserAccount newUser)
    {
        _accounts?.Add(newUser);
        Serializer.Save(this);
    }

    public void Delete(UserAccount entity)
    {
        _accounts.Remove(entity);
        Serializer.Save(this);
    }

    public UserAccount GetByEmail(string email)
    {
        return _accounts.FirstOrDefault(eq => eq.Email == email);
    }
}