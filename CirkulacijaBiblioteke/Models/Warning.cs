using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Models
{
    public class Warning
    {
        public DateTime Date {  get; set; }

        [JsonConstructor]
        public Warning(DateTime date)
        {
            Date = date;

        }
    }
}
