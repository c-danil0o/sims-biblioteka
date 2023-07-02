using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class BookViewModel : ViewModelBase
{
    public string Title { get; set; }
    public string Isbn { get; set; }
    public int Year { get; set; }
    public List<Author> Authors { get; set; }
    public Publisher Publisher { get; set; }
}