using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Models
{
    public enum MembershipType { Unemployed, Child, Employee, Pensioner, Student }
    public class Membership
    {
        public int Id { get; set; }
        public MembershipType Type { get; set; }
        public float Price { get; set; }
        public int MaxNumberOfBooks { get; set; }
        public int MembershipExtensionDeadline { get; set; }

        [JsonConstructor]
        public Membership(int id,MembershipType type, float price, int maxNumberOfBooks, int membershipExtensionDeadline)
        {
            Id = id;
            Type = type;
            Price = price;
            MaxNumberOfBooks = maxNumberOfBooks;
            MembershipExtensionDeadline = membershipExtensionDeadline;
        }
    }
}
