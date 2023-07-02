using System.Collections.Generic;
using System.Linq;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Utilities;
using Newtonsoft.Json.Linq;

namespace CirkulacijaBiblioteke.Repositories;

public class MemberRepository : ISerializable
{
    private readonly string _fileName = @".\..\..\..\Data\members.json";
    private List<Member>? _members;

    public MemberRepository()

    {
        _members = new List<Member>();
        Serializer.Load(this);
    }

    public string FileName()
    {
        return _fileName;
    }

    public IEnumerable<object>? GetList()
    {
        return _members;
    }

    public void Import(JToken token)
    {
        _members = token.ToObject<List<Member>>();
    }


    public IEnumerable<Member> GetAll()
    {
        return _members;
    }

    public void Insert(Member newMember)
    {
        _members?.Add(newMember);
        Serializer.Save(this);
    }

    public void Delete(Member entity)
    {
        _members.Remove(entity);
        Serializer.Save(this);
    }

    public Member GetById(string id)
    {
        return _members.FirstOrDefault(eq => eq.JMBG == id);
    }
}