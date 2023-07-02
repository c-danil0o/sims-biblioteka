using System;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class BookViewModel : ViewModelBase
{
    private Title _title;
    public String Title => _title.Name;
    public String Isbn => _title.ISBN;
    public int Year => _title.Year;
    public String Authors { get; set; }
    public String Publisher => _title.Publisher.ToString();

    public BookViewModel(Title title)
    {
        _title = title;
        var authors = "";
        foreach (var author in _title.Authors)
        {
            authors += author.ToString() + ", ";
        }

        Authors = authors;
    }
}