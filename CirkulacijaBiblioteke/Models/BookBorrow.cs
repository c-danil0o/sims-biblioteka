using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Models
{
    public class BookBorrow
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Returned { get; set; }
        public MembershipCard MembershipCard { get; set; }
        public BookInstance BookInstance { get; set; }

        [JsonConstructor]
        public BookBorrow(int id, DateTime creationDate, DateTime returnDate, bool returned, MembershipCard membershipCard, BookInstance bookInstance)
        {
            Id = id;
            CreationDate = creationDate;
            ReturnDate = returnDate;
            Returned = returned;
            MembershipCard = membershipCard;
            BookInstance = bookInstance;
        }
    }
}
