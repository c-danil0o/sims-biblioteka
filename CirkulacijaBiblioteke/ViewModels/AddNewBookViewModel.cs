using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class AddNewBookViewModel : ViewModelBase
{
    public string? Title { get; set; }
    public string? ISBN { get; set; }
    public int Year { get; set; } = 0;
    public string? UDK { get; set; }
    public string? Format { get; set; }
    public string? Cover { get; set; }
    public string? Authors { get; set; }
    private List<Author> _authorsList;
    public string? Publisher { get; set; }
    public string? Description { get; set; }
    public string? City { get; set; }
    public Regex authorsRegex = new Regex(@"([^,]+)");
    

    private Regex isbnRegex =
        new Regex(
            @"^(?:ISBN(?:-13)?:?\ )?(?=[0-9]{13}$|(?=(?:[0-9]+[-\ ]){4})[-\ 0-9]{17}$)97[89][-\ ]?[0-9]{1,5}[-\ ]?[0-9]+[-\ ]?[0-9]+[-\ ]?[0-9]$");

    public ICommand Confirm { get; private set; }
    public event EventHandler? OnRequestClose;
    private TitleService _titleService;
    
    public AddNewBookViewModel(TitleService titleService)
    {
        _titleService = titleService;
        Confirm = new DelegateCommand(o => ConfirmAdd(), o => CanConfirm());
    }

    public void MatchAuthors()
    {
        foreach (Match match in authorsRegex.Matches(Authors))
        {
            var firstName = "";
            var lastName = "";
            if (match.Value.Contains(" "))
            {

                var components = match.Value.Split(" ");
                for (int i = 0; i < components.Length; i++)
                {
                    if (i == 0){
                        firstName = components[i];
                        continue;
                    }
                    if (i>1)
                        lastName += " ";
                    lastName += components[i];
                }
                _authorsList.Add(new Author(firstName, lastName));
            }
            else
            {
                _authorsList.Add(new Author(match.Value, ""));
            }
            
               
        }

    }
    private void ConfirmAdd()
    {
        if (!isbnRegex.IsMatch(ISBN))
        {
            MessageBox.Show("ISBN format is not valid!");
            return;
        }

        if (_titleService.GetById(ISBN) != null)
        {
            MessageBox.Show("Book with same ISBN already exists!");
            return;
        }
        _authorsList = new List<Author>();
        MatchAuthors();
        var title = new Title(Title, Description, Format, Cover, UDK, ISBN, Year, _authorsList,
            new Publisher(Publisher, City));
        _titleService.AddTitle(title);
        OnRequestClose?.Invoke(this,EventArgs.Empty);   
    }

    private bool CanConfirm()
    {
        return Title != null && ISBN != null && Year != 0 && UDK != null && Format != null && Cover != null &&
               Authors != null && Publisher != null && City != null; 
    }
}