using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.Utilities;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class AddNewCopyViewModel: ViewModelBase
{

    public float Price { get; set; } = 0;
    public Copy.InstanceState? SelectedStatus { get; set; }
    public int Number { get; set; } = 0;
    public BookViewModel? SelectedBook { get; set; }
    public List<Copy.InstanceState> Status { get; set; }    
   
    private ObservableCollection<BookViewModel> _books;
  
    public ICommand Confirm { get; private set; }
    public event EventHandler? OnRequestClose;
    private TitleService _titleService;
    
    public AddNewCopyViewModel(TitleService titleService)
    {
        _titleService = titleService;
        Confirm = new DelegateCommand(o => ConfirmAdd(), o => CanConfirm());
        Status = new List<Copy.InstanceState>();
        LoadBooks();
        LoadStatus();
    }

    private void LoadBooks()
    {
        _books = new ObservableCollection<BookViewModel>();
        foreach (var title in _titleService.GetAll())
        {
            _books.Add(new BookViewModel(title));
        }  
    }

    private void LoadStatus()
    {
        Status.Add(Copy.InstanceState.Available);
        Status.Add(Copy.InstanceState.Damaged);
        Status.Add(Copy.InstanceState.Taken);
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
    private void ConfirmAdd()
    {
        for (int i = 0; i < Number; i++)
        {
            var copy = new Copy(IDGenerator.GetId(), Price, (Copy.InstanceState)SelectedStatus);
            _titleService.AddCopy(SelectedBook.Isbn, copy);
        }
        OnRequestClose?.Invoke(this,EventArgs.Empty);   
    }

    private bool CanConfirm()
    {
        return SelectedBook != null && Number > 0 && SelectedStatus != null && Price > 0;
    }
}