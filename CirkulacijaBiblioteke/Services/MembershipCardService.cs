using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Services
{
    public class MembershipCardService
    {
        private MembershipCardRepository _membershipCardRepository;

        public MembershipCardService(MembershipCardRepository membershipCardRepository)
        {
            _membershipCardRepository = membershipCardRepository;
        }

        public List<MembershipCard>? GetAll()
        {
            return _membershipCardRepository.GetAll() as List<MembershipCard>;
        }

        public MembershipCard GetById(int id)
        {
            return _membershipCardRepository.GetById(id);
        }

        public void AddMembershipCard(MembershipCard membershipCard)
        {
            _membershipCardRepository.Insert(membershipCard);
            DataChanged?.Invoke(this, new EventArgs());
        }

        public void Update(MembershipCard membershipCard)
        {
            var oldMember = _membershipCardRepository.GetById(membershipCard.Id);
            if (oldMember == null)
            {
                throw new KeyNotFoundException();
            }
            _membershipCardRepository.Delete(oldMember);
            _membershipCardRepository.Insert(membershipCard);
            DataChanged?.Invoke(this, new EventArgs());
        }

        public void Delete(int id)
        {
            _membershipCardRepository.Delete(_membershipCardRepository.GetById(id));
            DataChanged?.Invoke(this, new EventArgs());
        }

        public void ExtendCard(int id)
        {
            var card = GetById(id);
            card.ValidUntil = DateTime.Now.AddMonths(6);
            Update(card);
        }

        public event EventHandler? DataChanged;


    }
}
