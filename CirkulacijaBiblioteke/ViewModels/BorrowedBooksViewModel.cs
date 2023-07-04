using System;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class BorrowedBooksViewModel : ViewModelBase
{
    private BookBorrow _bookBorrow;
    private BookBorrowService _bookBorrowService;

    public String CreationDate { get; set; }
    public String ReturnDate { get; set; }
    public String CardId { get; set; }
    public String CopyInventoryNumber { get; set; }

    public BorrowedBooksViewModel(BookBorrow bookBorrow, BookBorrowService bookBorrowService)
    {
        _bookBorrow = bookBorrow;
        _bookBorrowService = bookBorrowService;

        var dateTimeFormat = "yyyy-MM-dd HH:mm";
        CreationDate = bookBorrow.CreationDate.ToString(dateTimeFormat);
        ReturnDate = bookBorrow.ReturnDate.ToString(dateTimeFormat);
        CardId = bookBorrow.MembershipCard.Membership.Id.ToString();
        CopyInventoryNumber = bookBorrow.Copy.InventoryNumber.ToString();
    }
    
    public override string ToString()
    {
        return CreationDate + " " + ReturnDate + " " + CardId + " " + CopyInventoryNumber;
    }
}