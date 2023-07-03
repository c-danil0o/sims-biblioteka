using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CirkulacijaBiblioteke.Models;

public class Title
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Format { get; set; }
    public string Cover { get; set; }
    public int UDK { get; set; }
    public string ISBN { get; set; }
    public int Year { get; set; }
    public List<Author> Authors { get; set; }
    public Publisher Publisher { get; set; }
    public List<Copy> Copies { get; set; }

    [JsonConstructor]
    public Title(string name, string description, string format, string cover, int udk, string isbn, int year, List<Author> authors, Publisher publisher, List<Copy> copies)
    {
        Name = name;
        Description = description;
        Format = format;
        Cover= cover;
        UDK = udk;
        ISBN = isbn;
        Year = year;
        Authors = authors;
        Publisher = publisher;
        Copies = copies;
    }
}