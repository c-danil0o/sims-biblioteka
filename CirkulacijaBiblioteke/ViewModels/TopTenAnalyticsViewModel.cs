using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class TopTenAnalyticsViewModel : ViewModelBase
{
    private ObservableCollection<RangBookViewModel> _books;
    private TitleService _titleService;
    private BookBorrowService _bookBorrowService;
    private string _searchText = "";


    public TopTenAnalyticsViewModel(TitleService titleService, BookBorrowService bookBorrowService)
    {
        _titleService = titleService;
        _bookBorrowService = bookBorrowService;
        _books = new ObservableCollection<RangBookViewModel>();
        var sortedCount = _bookBorrowService.GetBorrowCountForLastMonth().OrderBy(x=>x.Value).ToList();
        var count = sortedCount.Count >= 10 ? 10 : sortedCount.Count;
        for (int i = 0; i < count; i++)
        {
            _books.Add(new  RangBookViewModel(_titleService.GetById(sortedCount[sortedCount.Count - i - 1].Key), i+1, sortedCount[sortedCount.Count-i -1].Value));
        }

    }



 
    public IEnumerable<RangBookViewModel> Books
    {
        get => _books;
        set
        {
            _books = new ObservableCollection<RangBookViewModel>(value);
            OnPropertyChanged();
        }
    }
}