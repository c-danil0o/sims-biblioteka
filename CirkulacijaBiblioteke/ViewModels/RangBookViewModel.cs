using System;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class RangBookViewModel : ViewModelBase
{
    private Title _title;
    public String Title => _title.Name;
    public int Year => _title.Year;
    public String Authors { get; set; }
    public String Publisher => _title.Publisher.ToString();
    public int Position { get; set; }
    public int BorrowCount { get; set; }

    public RangBookViewModel(Title title, int position, int borrowCount)
    {
        Position = position;
        BorrowCount = borrowCount;
        _title = title;
        Authors = string.Join(',', _title.Authors);
        
    }
}