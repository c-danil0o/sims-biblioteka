using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Repositories;

public class TitleRepository : ISerializable
{
    private readonly string _fileName = @".\..\..\..\Data\members.json";
    private List<Title>? _titles;

    public TitleRepository()
    {
        _titles = new List<Title>();
        Serializer.Load(this);
    }

    public string FileName()
    {
        return _fileName;
    }

    public IEnumerable<object>? GetList()
    {
        return _titles;
    }

    public void Import(JToken token)
    {
        _titles = token.ToObject<List<Title>>();
    }


    public IEnumerable<Title> GetAll()
    {
        return _titles;
    }

    public void Insert(Title newTitle)
    {
        _titles?.Add(newTitle);
        Serializer.Save(this);
    }

    public void Delete(Title entity)
    {
        _titles.Remove(entity);
        Serializer.Save(this);
    }

    public Title GetById(string id)
    {
        return _titles.FirstOrDefault(eq => eq.ISBN == id);
    }
}