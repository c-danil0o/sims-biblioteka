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
    public string TipKorica { get; set; }
    public int UDK { get; set; }
    public string ISBN { get; set; }
    public int Year { get; set; }
    public List<Author> Authors { get; set; }
    public Publisher Publisher { get; set; }
    public List<BookInstance> BookInstances { get; set; }

    [JsonConstructor]
    public Title(string name, string description, string format, string tipKorica, int udk, string isbn, int year, List<Author> authors, Publisher publisher, List<BookInstance> bookInstances)
    {
        Name = name;
        Description = description;
        Format = format;
        TipKorica = tipKorica;
        UDK = udk;
        ISBN = isbn;
        Year = year;
        Authors = authors;
        Publisher = publisher;
        BookInstances = bookInstances;
    }
}