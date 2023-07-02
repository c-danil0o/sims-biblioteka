using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Repositories
{
    public class MembershipRepository : ISerializable
    {
        private readonly string _fileName = @".\..\..\..\..\Data\membership.json";
        private List<Membership>? _memberships;

        public MembershipRepository() 
        {
            _memberships = new List<Membership>();
            Serializer.Load(this);
        }
        public string FileName()
        {
            return _fileName;
        }

        public IEnumerable<object>? GetList()
        {
            return _memberships;
        }

        public void Import(JToken token)
        {
            _memberships = token.ToObject<List<Membership>>();
        }


        public IEnumerable<Membership> GetAll()
        {
            return _memberships;
        }

        public void Insert(Membership newMembership)
        {
            _memberships?.Add(newMembership);
            Serializer.Save(this);
        }

        public void Delete(Membership entity)
        {
            _memberships.Remove(entity);
            Serializer.Save(this);
        }

        public Membership GetById(int id)
        {
            return _memberships.FirstOrDefault(ms => ms.Id == id);
        }
    }
}
