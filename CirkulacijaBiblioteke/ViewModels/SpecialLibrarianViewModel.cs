﻿using System.Windows.Input;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class SpecialLibrarianViewModel : ViewModelBase
{
    private object _currentView;

    private MemberService _memberService;
    private UserAccountService _userAccountService;
    private MembershipService _membershipService;
    private MembershipCardService _membershipCardService;
    private BookBorrowService _bookBorrowService;
    private TitleService _titleService;
    private PaymentService _paymentService;
    
    
    public SpecialLibrarianViewModel(MemberService memberService, UserAccountService userAccountService, MembershipService membershipService,MembershipCardService membershipCardService, TitleService titleService, BookBorrowService bookBorrowService, PaymentService paymentService)
    {
        _memberService = memberService;
        _userAccountService = userAccountService;
        _membershipService = membershipService;
        _membershipCardService = membershipCardService;
        _titleService = titleService;
        _bookBorrowService = bookBorrowService;
        _paymentService = paymentService;
        _currentView = new SBooksPaneViewModel(_titleService);

        NewAccountCommand = new DelegateCommand(o => NewAccountView());
        NewMemberCardCommand = new DelegateCommand(o => NewMemberCardView());
        ReturnBookCommand = new DelegateCommand(o => ReturnBookView());
        
        ViewBooksCommand = new DelegateCommand(o => BooksView());
        ViewCopiesCommand = new DelegateCommand(o => CopiesView());
    }



    public ICommand ViewBooksCommand { get; private set; }
    public ICommand ViewCopiesCommand { get; private set; }
    public ICommand NewAccountCommand { get; private set; }
    public ICommand NewMemberCardCommand { get; private set; }
    public ICommand ReturnBookCommand { get; private set; }



    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    private void BooksView()
    {
        CurrentView = new SBooksPaneViewModel(_titleService);
    }

    private void CopiesView()
    {
        CurrentView = new SCopiesPaneViewModel(_titleService);
    }
    
    private void NewMemberCardView()
    {
        CurrentView = new NewMemberCardViewModel(_memberService, _membershipService, _membershipCardService, _titleService, _bookBorrowService);
    }

    private void NewAccountView()
    {
        CurrentView = new NewAccountViewModel(_memberService, _userAccountService);
    }

    private void ReturnBookView()
    {
        CurrentView = new ReturnBookViewModel(_bookBorrowService, _titleService, _paymentService, _membershipCardService);
    }
    
   
}