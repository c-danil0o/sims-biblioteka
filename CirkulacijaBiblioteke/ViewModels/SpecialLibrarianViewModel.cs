using System.Windows.Input;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class SpecialLibrarianViewModel : ViewModelBase
{
    private object _currentView;

    private TitleService _titleService;
    

    public SpecialLibrarianViewModel(TitleService titleService)
    {
        _titleService = titleService;

        ViewBooksCommand = new DelegateCommand(o => BooksView());
        ViewCopiesCommand = new DelegateCommand(o => CopiesView());
        

        _currentView = new SBooksPaneViewModel(_titleService);
    }

    public ICommand ViewBooksCommand { get; private set; }
    public ICommand ViewCopiesCommand { get; private set; }



    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    private void BooksView()
    {
        CurrentView = new SBooksPaneViewModel(_titleService);
    }

    private void CopiesView()
    {
        CurrentView = new SCopiesPaneViewModel(_titleService);
    }
    
   
}