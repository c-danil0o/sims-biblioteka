using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Models;

public class Publisher
{
    public string Name { get; set; }
    public string City { get; set; }
    public Publisher(string name, string city)
    {
        Name = name;
        City = city;
    }
}