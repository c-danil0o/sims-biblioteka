using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class SCopiesPaneViewModel: ViewModelBase
{
    private  ObservableCollection<CopyViewModel> _allBooks;
    private ObservableCollection<CopyViewModel> _filteredBooks;
    private ObservableCollection<CopyViewModel> _books;
    private TitleService _titleService;
    private string _searchText = "";
    public CopyViewModel? SelectedCopy { get; set; }
    public ICommand AddCopyCommand { get; private set; }
    public ICommand RemoveCopyCommand { get; private set; }


    public SCopiesPaneViewModel(TitleService titleService)
    {
        _titleService = titleService;
        _titleService.DataChanged += (sender, args) => UpdateTable(true);
        _allBooks = new ObservableCollection<CopyViewModel>();

        LoadBooks();
        AddCopyCommand = new DelegateCommand(o => AddCopy());
        RemoveCopyCommand = new DelegateCommand(o => RemoveCopy(), o => CanExecute());
        _books = _allBooks;
        
    }

    private void LoadBooks()
    {
        _allBooks = new ObservableCollection<CopyViewModel>();
        foreach (var title in _titleService.GetAll())
        {
            if (title.Copies != null)
                foreach (var copy in title.Copies)
                {
                    _allBooks.Add(new CopyViewModel(title.Name, string.Join(',', title.Authors), copy.InventoryNumber,
                        copy.State.ToString(), copy.Price, title.ISBN));
                }
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

 
    public IEnumerable<CopyViewModel> Books
    {
        get => _books;
        set
        {
            _books = new ObservableCollection<CopyViewModel>(value);
            OnPropertyChanged();
        }
    }

    private void AddCopy()
    {
        var vm = new AddNewCopyViewModel(_titleService);
        
        var window = new AddNewCopyView(){DataContext = vm};
        vm.OnRequestClose += (s,e) =>window.Close();
        window.Show();
    }
    
    private void RemoveCopy()
    {
        if (SelectedCopy.State == "Taken")
        {
            MessageBox.Show("You can't delete TAKEN copy!");
            return;
        }
        _titleService.DeleteCopy(SelectedCopy.Isbn, SelectedCopy.InventoryNumber);
        MessageBox.Show("Copy deleted.");
    }

    private bool CanExecute()
    {
        return SelectedCopy != null;
    }

    private void UpdateTable(bool newAdded=false)
    {
        if (newAdded)
            LoadBooks();
        _filteredBooks = _allBooks;
        var wh = _filteredBooks.Intersect(UpdateTableFromSearch());
        Books = wh;
    }

    private ObservableCollection<CopyViewModel> UpdateTableFromSearch()
    {
        if (_searchText != "")
            return new ObservableCollection<CopyViewModel>(Search(_searchText));
        return _allBooks;
    }


    public IEnumerable<CopyViewModel> Search(string inputText)
    {
        return _allBooks.Where(item => item.ToString().Contains(inputText));
    }
    
}