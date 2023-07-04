/* using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class ReturnBookViewModel : ViewModelBase
{
    private BookBorrowService _bookBorrowService;
    private ObservableCollection<BorrowedBooksViewModel> _borrowedBooks;
    private ObservableCollection<BorrowedBooksViewModel> _allBorrowedBooks;
    private ObservableCollection<BorrowedBooksViewModel> _filteredBorrowedBooks;
    private string _searchText = "";
    
    public ICommand ReturnBookCommand { get; set; }

    public ReturnBookViewModel(BookBorrowService bookBorrowService)
    {
        _bookBorrowService = bookBorrowService;
        _bookBorrowService.DataChanged += (sender, args) => UpdateTable();
        _allBorrowedBooks = new ObservableCollection<BorrowedBooksViewModel>();
        foreach (var bookBorrow in _bookBorrowService.GetAll())
        {
            if (!bookBorrow.Returned)
                _allBorrowedBooks.Add(new BorrowedBooksViewModel(bookBorrow, _bookBorrowService));
        }

        _borrowedBooks = _allBorrowedBooks;
        
        ReturnBookCommand = new DelegateCommand(o => ReturnBookCommand());
    }
    
    public BorrowedBooksViewModel SelectedBookBorrow { get; set; }
    
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
    
    public IEnumerable<BorrowedBooksViewModel> BookBorrows
    {
        get => _borrowedBooks;
        set
        {
            _borrowedBooks = new ObservableCollection<BorrowedBooksViewModel>(value);
            OnPropertyChanged();
        }
    }
    private void UpdateTable()
    {
        _allBorrowedBooks = new ObservableCollection<BorrowedBooksViewModel>();
        foreach (var bookBorrow in _bookBorrowService.GetAll())
        {
            _allBorrowedBooks.Add(new BorrowedBooksViewModel(bookBorrow, _bookBorrowService));
        }
        _filteredBorrowedBooks = _allBorrowedBooks;
        var aux = _filteredBorrowedBooks.Intersect(UpdateTableFromSearch());
        BookBorrows = aux;
    }
    
    private ObservableCollection<BorrowedBooksViewModel> UpdateTableFromSearch()
    {
        if (_searchText != "")
            return new ObservableCollection<BorrowedBooksViewModel>(Search(_searchText));
        return _allBorrowedBooks;
    }

    public IEnumerable<BorrowedBooksViewModel> Search(string inputText)
    {
        return _allBorrowedBooks.Where(item => item.ToString().Contains(inputText));
    }
} */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels
{
    public class ReturnBookViewModel : ViewModelBase
    {
        private readonly BookBorrowService _bookBorrowService;
        private ObservableCollection<BorrowedBooksViewModel> _borrowedBooks;
        private ObservableCollection<BorrowedBooksViewModel> _allBorrowedBooks;
        private ObservableCollection<BorrowedBooksViewModel> _filteredBorrowedBooks;
        private string _searchText = "";

        public ICommand ReturnBookCommand { get; }

        public ReturnBookViewModel(BookBorrowService bookBorrowService)
        {
            _bookBorrowService = bookBorrowService;
            _bookBorrowService.DataChanged += (sender, args) => UpdateTable();
            _allBorrowedBooks = new ObservableCollection<BorrowedBooksViewModel>(
                _bookBorrowService.GetAll()
                    .Where(bookBorrow => !bookBorrow.Returned)
                    .Select(bookBorrow => new BorrowedBooksViewModel(bookBorrow, _bookBorrowService))
            );
            _borrowedBooks = _allBorrowedBooks;

            ReturnBookCommand = new DelegateCommand(ReturnBook);
        }

        public BorrowedBooksViewModel SelectedBookBorrow { get; set; }

        public string SearchBox
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    UpdateTable();
                    OnPropertyChanged();
                }
            }
        }

        public IEnumerable<BorrowedBooksViewModel> BookBorrows
        {
            get => _borrowedBooks;
            set
            {
                _borrowedBooks = new ObservableCollection<BorrowedBooksViewModel>(value);
                OnPropertyChanged();
            }
        }

        private void UpdateTable()
        {
            _allBorrowedBooks = new ObservableCollection<BorrowedBooksViewModel>(
                _bookBorrowService.GetAll()
                    .Select(bookBorrow => new BorrowedBooksViewModel(bookBorrow, _bookBorrowService))
            );
            _filteredBorrowedBooks = _allBorrowedBooks;
            var aux = _filteredBorrowedBooks.Intersect(UpdateTableFromSearch());
            BookBorrows = aux;
        }

        private ObservableCollection<BorrowedBooksViewModel> UpdateTableFromSearch()
        {
            if (!string.IsNullOrEmpty(_searchText))
                return new ObservableCollection<BorrowedBooksViewModel>(Search(_searchText));
            return _allBorrowedBooks;
        }

        public IEnumerable<BorrowedBooksViewModel> Search(string inputText)
        {
            return _allBorrowedBooks.Where(item => item.ToString().Contains(inputText));
        }

        private void ReturnBook(object parameter)
        {
            
        }
    }
}
