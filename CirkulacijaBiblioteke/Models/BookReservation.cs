using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Models
{
    public class BookReservation
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Expired { get; set; }
        public DateTime NotificationDate { get; set; } 
        public MembershipCard MembershipCard { get; set; }
        public BookInstance BookInstance { get; set; }

        [JsonConstructor]
        public BookReservation(int id, DateTime creationDate, bool expired, DateTime notificationDate, MembershipCard membershipCard, BookInstance bookInstance)
        {
            Id = id;
            CreationDate = creationDate;
            Expired = expired;
            NotificationDate = notificationDate;
            MembershipCard = membershipCard;
            BookInstance = bookInstance;
        }
    }
}
