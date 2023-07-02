using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Models
{
    public class Fine
    {
        public DateTime Date {  get; set; }
        public bool Paid { get; set; }

        [JsonConstructor]
        public Fine(DateTime date, bool paid)
        {
            Date = date;
            Paid = paid;
        }
    }
}
