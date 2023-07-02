using System.Windows.Input;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class ArchivistViewModel : ViewModelBase
{
    private object _currentView;

    private TitleService _titleService;
    

    public ArchivistViewModel(TitleService titleService)
    {
        _titleService = titleService;

        ViewBooksCommand = new DelegateCommand(o => BooksView());
        AddTitleCommand = new DelegateCommand(o => AddTitleView());
        AddBookInstanceCommand = new DelegateCommand(o => AddBookInstanceView());

        _currentView = new BooksPaneViewModel(_titleService);
    }

    public ICommand ViewBooksCommand { get; private set; }
    public ICommand AddTitleCommand { get; private set; }
    public ICommand AddBookInstanceCommand { get; private set; }



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
        CurrentView = new BooksPaneViewModel(_titleService);
    }

    private void AddTitleView()
    {
        CurrentView = new AddTitleViewModel();
    }

    private void AddBookInstanceView()
    {
        CurrentView = new AddBookInstanceViewModel();
    }
    
   
}