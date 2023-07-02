using System.Windows.Input;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class GuestViewModel : ViewModelBase
{
    private object _currentView;


    

    public GuestViewModel()
    {
        

        ViewBooksCommand = new DelegateCommand(o => BooksView());

        _currentView = new BooksPaneViewModel();
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
        CurrentView = new BooksPaneViewModel();
    }
}