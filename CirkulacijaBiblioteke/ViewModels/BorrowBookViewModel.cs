using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.Utilities;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class BorrowBookViewModel: ViewModelBase
{
    private readonly ObservableCollection<BookViewModel> _allBooks;
    private ObservableCollection<BookViewModel> _filteredBooks;
    private ObservableCollection<BookViewModel> _books;
    private TitleService _titleService;
    private MembershipCard _membershipCard;
    private BookBorrowService _bookBorrowService;
    private string _searchText = "";

    public ICommand BorrowCommand { get; set; }

    public BorrowBookViewModel(TitleService titleService, BookBorrowService bookBorrowService, MembershipCard membershipCard)
    {
        _titleService = titleService;
        _bookBorrowService = bookBorrowService;
        _membershipCard = membershipCard;
        _titleService.DataChanged += (sender, args) => UpdateTable();
        _allBooks = new ObservableCollection<BookViewModel>();
        foreach (var title in _titleService.GetAll())
        {
            _allBooks.Add(new BookViewModel(title));
        }

        _books = _allBooks;
        BorrowCommand = new DelegateCommand(o => Borrow());
    }

    public BookViewModel SelectedBook { get; set; } 
    
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

    private void Borrow()
    {
        if (SelectedBook == null)
        {
            MessageBox.Show("None selected", "Error", MessageBoxButton.OK);
            return;
        }

        var copy = _titleService.GetAvailableCopy(SelectedBook.Isbn);
        if (copy == null)
        {
            MessageBox.Show("No copies available", "Warning", MessageBoxButton.OK);
            return;
        }
        
        copy.State = Copy.InstanceState.Taken;
        _titleService.UpdateCopy(SelectedBook.Isbn, copy);
        
        
        var bookBorrow = new BookBorrow(IDGenerator.GetId(), DateTime.Now, DateTime.Now.AddDays(-1), false,
            _membershipCard, copy);
        _bookBorrowService.AddBookBorrow(bookBorrow);

        MessageBox.Show("Book successfully borrowed", "Notification", MessageBoxButton.OK);
        var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this);
        window?.Close();
    }
}