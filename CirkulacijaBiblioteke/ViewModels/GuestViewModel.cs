using System.Windows.Input;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class GuestViewModel : ViewModelBase
{
    private object _currentView;

    private TitleService _titleService;
    

    public GuestViewModel(TitleService titleService)
    {
        _titleService = titleService;

        ViewBooksCommand = new DelegateCommand(o => BooksView());

        _currentView = new BooksPaneViewModel(_titleService);
    }

    public ICommand ViewBooksCommand { get; private set; }



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
}