using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Models
{
    public enum PaymentType { DamagingBook, MembershipPayment, DelayedReturn}
    public class Payment
    {
       
        public int Id { get; set; }
        public float Price { get; set; }
        public PaymentType Type { get; set; }
        public MembershipCard MembershipCard { get; set; }
        public Payment(int id, float price, PaymentType type, MembershipCard membershipCard)
        {
            Id = id;
            Price = price;
            Type = type;
            MembershipCard = membershipCard;
        }
    }
}
