using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Models;

public class Title
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Format { get; set; }
    public string TipKorica { get; set; }
    public int UDK { get; set; }
    public string ISBN { get; set; }
    public int Godina { get; set; }
    public List<Author> Authors { get; set; }
    public Publisher Publisher { get; set; }
    public List<BookInstance> BookInstances { get; set; }


    public Title(string name, string description, string format, string tipKorica, int udk, string isbn, int godina, List<Author> authors, Publisher publisher, List<BookInstance> bookInstances)
    {
        Name = name;
        Description = description;
        Format = format;
        TipKorica = tipKorica;
        UDK = udk;
        ISBN = isbn;
        Godina = godina;
        Authors = authors;
        Publisher = publisher;
        BookInstances = bookInstances;
    }
}