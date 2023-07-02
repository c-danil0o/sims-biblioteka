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
    public class MembershipCardRepository : ISerializable
    {
        private readonly string _fileName = @".\..\..\..\Data\membershipCard.json";
        private List<MembershipCard>? _membershipCards;

        public MembershipCardRepository()
        {
            _membershipCards = new List<MembershipCard>();
            Serializer.Load(this);
        }

        public string FileName()
        {
            return _fileName;
        }

        public IEnumerable<object>? GetList()
        {
            return _membershipCards;
        }

        public void Import(JToken token)
        {
            _membershipCards = token.ToObject<List<MembershipCard>>();
        }

        public IEnumerable<MembershipCard> GetAll()
        {
            return _membershipCards;
        }

        public void Insert(MembershipCard newMembershipCard)
        {
            _membershipCards?.Add(newMembershipCard);
            Serializer.Save(this);
        }

        public void Delete(MembershipCard entity)
        {
            _membershipCards.Remove(entity);
            Serializer.Save(this);
        }

        public MembershipCard GetById(int id)
        {
            return _membershipCards.FirstOrDefault(msc => msc.Id == id);
        }
    }
}
