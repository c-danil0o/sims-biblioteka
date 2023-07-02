using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class BooksPaneViewModel: ViewModelBase
{
     private readonly ObservableCollection<BookViewModel> _allBooks;
    private ObservableCollection<BookViewModel> _filteredBooks;
    private ObservableCollection<BookViewModel> _books;
    private string _searchText = "";


    public BooksPaneViewModel()
    {
        
    }
    /*public EquipmentPaneViewModel(IInventoryService inventoryService)
    {
        _lock = new object();
        _inventoryService = inventoryService;
        _inventoryService.DataChanged += (s, e) => UpdateTable();
        _allInventory = new ObservableCollection<InventoryViewModel>();
        foreach (var inventoryItem in _inventoryService.GetAll())
            _allInventory.Add(new InventoryViewModel(inventoryItem));
        _inventory = _allInventory;
        EquipmentTypes = new ObservableCollection<string>();
        RoomTypes = new ObservableCollection<string>();
        Quantities = new ObservableCollection<string>();
        BindingOperations.EnableCollectionSynchronization(_inventory, _lock);
        LoadComboBoxCollections();
    }*/

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
        get => _allBooks;
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