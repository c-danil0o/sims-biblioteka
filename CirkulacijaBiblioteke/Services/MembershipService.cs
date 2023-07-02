using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Services
{
    public class MembershipService
    {
        private MembershipRepository _membershipRepository;

        public MembershipService(MembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public List<Membership>? GetAll()
        {
            return _membershipRepository.GetAll() as List<Membership>;
        }

        public void AddMember(Membership membership)
        {
            _membershipRepository.Insert(membership);
            DataChanged?.Invoke(this, new EventArgs());
        }

        public void Update(int id, Membership membership)
        {
            var oldMember = _membershipRepository.GetById(id);
            if (oldMember == null)
            {
                throw new KeyNotFoundException();
            }
            _membershipRepository.Delete(oldMember);
            _membershipRepository.Insert(membership);
            DataChanged?.Invoke(this, new EventArgs());
        }

        public void Delete(int id)
        {
            _membershipRepository.Delete(_membershipRepository.GetById(id));
            DataChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler? DataChanged;
    }
}
