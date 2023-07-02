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

        public void AddMember(MembershipCard membershipCard)
        {
            _membershipCardRepository.Insert(membershipCard);
            DataChanged?.Invoke(this, new EventArgs());
        }

        public void Update(int id, MembershipCard membershipCard)
        {
            var oldMember = _membershipCardRepository.GetById(id);
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

        public event EventHandler? DataChanged;
    }
}
