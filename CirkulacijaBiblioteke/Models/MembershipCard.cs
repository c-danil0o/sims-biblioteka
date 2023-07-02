using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Models
{
    public enum MembershipCardStatus { Active, Blocked, Expired, Suspended }
    public class MembershipCard
    {
        public Membership Membership { get; set; }
        public int Id { get; set; }
        public DateTime ValidUntil { get; set; }
        public MembershipCardStatus Status { get; set; }

        public List<Warning> Warnings { get; set; }
        public List<Fine> Fines { get; set; }

        [JsonConstructor]
        public MembershipCard(Membership membership, int id, DateTime validUntil, MembershipCardStatus status, List<Warning> warnings, List<Fine> fines) 
        { 
            Membership = membership;
            Id = id;
            ValidUntil = validUntil;
            Status = status;
            Warnings = warnings;
            Fines = fines;
        }

        public void UpdateMembership(Membership membership) 
        {
            Membership = membership;
        }


    }

}
