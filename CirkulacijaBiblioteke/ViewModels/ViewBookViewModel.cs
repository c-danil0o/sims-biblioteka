using CirkulacijaBiblioteke.Models;

namespace CirkulacijaBiblioteke.ViewModels;

public class ViewBookViewModel
{
    private Title _title;
    public string? Title => _title.Name;
    public string? ISBN => _title.ISBN;
    public int Year => _title.Year;
    public string? UDK => _title.UDK;
    public string? Format => _title.Format;
    public string? Cover => _title.Cover;
    public string? Authors => string.Join(',', _title.Authors);
    public string? Publisher => _title.Publisher.Name;
    public string? Description => _title.Description;
    public string? City => _title.Publisher.City;

    public ViewBookViewModel(Title title)
    {
        _title = title;
    }
}