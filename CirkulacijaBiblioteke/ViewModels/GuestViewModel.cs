using System.Windows.Input;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class GuestViewModel : ViewModelBase
{
    private object _currentView;

    private TitleService _titleService;
    private BookBorrowService _bookBorrowService;
    

    public GuestViewModel(TitleService titleService, BookBorrowService bookBorrowService)
    {
        _titleService = titleService;
        _bookBorrowService = bookBorrowService;

        ViewBooksCommand = new DelegateCommand(o => BooksView());
        ViewTopTenCommand = new DelegateCommand(o => TopTenView());

        _currentView = new BooksPaneViewModel(_titleService);
    }

    public ICommand ViewBooksCommand { get; private set; }
    public ICommand ViewTopTenCommand { get; private set; }



    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public void BooksView()
    {
        CurrentView = new BooksPaneViewModel(_titleService);
    }
    
    public void TopTenView()
    {
        CurrentView = new TopTenAnalyticsViewModel(_titleService, _bookBorrowService);
    }
}