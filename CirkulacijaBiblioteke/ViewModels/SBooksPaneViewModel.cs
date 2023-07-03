using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class SBooksPaneViewModel: ViewModelBase
{
    private  ObservableCollection<BookViewModel> _allBooks;
    private ObservableCollection<BookViewModel> _filteredBooks;
    private ObservableCollection<BookViewModel> _books;
    private TitleService _titleService;
    private string _searchText = "";
    public BookViewModel? SelectedBook { get; set; }
    public ICommand AddBookCommand { get; private set; }
    public ICommand RemoveBookCommand { get; private set; }
    public ICommand ViewBookCommand { get; private set; }


    public SBooksPaneViewModel(TitleService titleService)
    {
        _titleService = titleService;
        _titleService.DataChanged += (sender, args) => UpdateTable(true);
        _allBooks = new ObservableCollection<BookViewModel>();

        LoadBooks();
        AddBookCommand = new DelegateCommand(o => AddBook());
        RemoveBookCommand = new DelegateCommand(o => RemoveBook(), o => CanExecute());
        ViewBookCommand = new DelegateCommand(o => ViewBook(), o => CanExecute());

        _books = _allBooks;
        
    }

    private void LoadBooks()
    {
        _allBooks = new ObservableCollection<BookViewModel>();
        foreach (var title in _titleService.GetAll())
        {
            _allBooks.Add(new BookViewModel(title));
        }  
    }

    public string SearchBox
    {
        get => _searchText;
        set
        {
            _searchText = value;
            UpdateTable();
            OnPropertyChanged("Search");
        }
    }

 
    public IEnumerable<BookViewModel> Books
    {
        get => _books;
        set
        {
            _books = new ObservableCollection<BookViewModel>(value);
            OnPropertyChanged();
        }
    }

    private void AddBook()
    {
        var vm = new AddNewBookViewModel(_titleService);
        
        var window = new AddNewBookView(){DataContext = vm};
        vm.OnRequestClose += (s,e) =>window.Close();
        window.Show();
    }

    private void ViewBook()
    {
        var vm = new ViewBookViewModel(_titleService.GetById(SelectedBook.Isbn));
        var window = new ViewBookView() { DataContext = vm };
        window.Show();
    }
    private void RemoveBook()
    {
        _titleService.Delete(SelectedBook.Isbn);
        MessageBox.Show("Book deleted.");
    }

    private bool CanExecute()
    {
        return SelectedBook != null;
    }

    private void UpdateTable(bool newAdded=false)
    {
        if (newAdded)
            LoadBooks();
        _filteredBooks = _allBooks;
        var wh = _filteredBooks.Intersect(UpdateTableFromSearch());
        Books = wh;
    }

    private ObservableCollection<BookViewModel> UpdateTableFromSearch()
    {
        if (_searchText != "")
            return new ObservableCollection<BookViewModel>(Search(_searchText));
        return _allBooks;
    }


    public IEnumerable<BookViewModel> Search(string inputText)
    {
        return _allBooks.Where(item => item.ToString().Contains(inputText));
    }
    
}