using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class BooksPaneViewModel: ViewModelBase
{
    private readonly ObservableCollection<BookViewModel> _allBooks;
    private ObservableCollection<BookViewModel> _filteredBooks;
    private ObservableCollection<BookViewModel> _books;
    private TitleService _titleService;
    private string _searchText = "";


    public BooksPaneViewModel(TitleService titleService)
    {
        _titleService = titleService;
        _titleService.DataChanged += (sender, args) => UpdateTable();
        _allBooks = new ObservableCollection<BookViewModel>();
        foreach (var title in _titleService.GetAll())
        {
            _allBooks.Add(new BookViewModel(title));
        }

        _books = _allBooks;
        
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

    private void UpdateTable()
    {
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