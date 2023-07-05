using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Repositories;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;
using static System.Reflection.Metadata.BlobBuilder;

namespace CirkulacijaBiblioteke.ViewModels;

public class NewMemberCardViewModel : ViewModelBase
{
    private MemberService _memberService;
    private MembershipService _membershipService;
    private MembershipCardService _membershipCardService;
    private TitleService _titleService;
    private BookBorrowService _bookBorrowService;
    private ObservableCollection<MemberViewModel> _allMembers;
    private ObservableCollection<MemberViewModel> _filteredMembers;
    private ObservableCollection<MemberViewModel> _members;
    private string _searchText = "";

    public ICommand CreateMemberCardCommand { get; set; }
    public ICommand ExtendMemberCardCommand { get; set; }
    public ICommand ChangeMemberCardCommand { get; set; }
    public ICommand BorrowBookCommand { get; set; }

    public NewMemberCardViewModel(MemberService memberService, MembershipService membershipService, MembershipCardService membershipCardService, TitleService titleService, BookBorrowService bookBorrowService)
    {
        _memberService = memberService;
        _membershipService = membershipService;
        _membershipCardService = membershipCardService;
        _bookBorrowService = bookBorrowService;
        _titleService = titleService;
        _memberService.DataChanged += (sender, args) => UpdateTable();
        _allMembers = new ObservableCollection<MemberViewModel>();
        foreach (var member in _memberService.GetAll())
        {
            _allMembers.Add(new MemberViewModel(member, membershipCardService));
        }
        _members = _allMembers;
        CreateMemberCardCommand = new DelegateCommand(o => CreateMemberCard());
        ExtendMemberCardCommand = new DelegateCommand(o => ExtendMemberCard());
        ChangeMemberCardCommand = new DelegateCommand(o => ChangeMemberCard());
        BorrowBookCommand = new DelegateCommand(o => BorrowBook());
    }

    public MemberViewModel SelectedMember { get; set; } 

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

    public IEnumerable<MemberViewModel> Members
    {
        get => _members;
        set
        {
            _members = new ObservableCollection<MemberViewModel>(value);
            OnPropertyChanged();
        }
    }



    private void UpdateTable()
    {
        _allMembers = new ObservableCollection<MemberViewModel>();
        foreach (var member in _memberService.GetAll())
        {
            _allMembers.Add(new MemberViewModel(member, _membershipCardService));
        }
        _filteredMembers = _allMembers;
        var wh = _filteredMembers.Intersect(UpdateTableFromSearch());
        Members = wh;
    }


    private ObservableCollection<MemberViewModel> UpdateTableFromSearch()
    {
        if (_searchText != "")
            return new ObservableCollection<MemberViewModel>(Search(_searchText));
        return _allMembers;
    }

    public IEnumerable<MemberViewModel> Search(string inputText)
    {
        return _allMembers.Where(item => item.ToString().Contains(inputText));
    }

    private void CreateMemberCard()
    {
        if (SelectedMember == null)
        {
            MessageBox.Show("None selected", "Error", MessageBoxButton.OK);
            return;
        }
        var member = _memberService.GetById(SelectedMember.JMBG);
        if (member.CardNumber != -1)
        {
            MessageBox.Show("Member already has a card", "Error", MessageBoxButton.OK);
            return;
        }
        var window = new CreateMemberCardView()
        {
            DataContext = new CreateMemberCardViewModel(_membershipService,_membershipCardService, _memberService, member.JMBG)
        };
        window.Show();

    }

    private void ExtendMemberCard()
    {
        if (SelectedMember == null)
        {
            MessageBox.Show("None selected", "Error", MessageBoxButton.OK);
            return;
        }
        var member = _memberService.GetById(SelectedMember.JMBG);
        if (member.CardNumber == -1)
        {
            MessageBox.Show("Member dont have a card", "Error", MessageBoxButton.OK);
            return;
        }
        var card = _membershipCardService.GetById(member.CardNumber);
        if (card.ValidUntil > DateTime.Now)
        {
            MessageBox.Show("Card hasn't expired yet", "Error", MessageBoxButton.OK);
            return;
        }
        _membershipCardService.ExtendCard(member.CardNumber);
        MessageBox.Show("Card extended successfully", "Error", MessageBoxButton.OK);
        UpdateTable();
    }

    private void ChangeMemberCard()
    {
        if (SelectedMember == null)
        {
            MessageBox.Show("None selected", "Error", MessageBoxButton.OK);
            return;
        }
        var member = _memberService.GetById(SelectedMember.JMBG);
        if (member.CardNumber == -1)
        {
            MessageBox.Show("Member dont have a card", "Error", MessageBoxButton.OK);
            return;
        }
        var window = new CreateMemberCardView()
        {
            DataContext = new CreateMemberCardViewModel(_membershipService, _membershipCardService, _memberService, member.JMBG)
        };
        window.Show();
        UpdateTable();
    }

    private void BorrowBook()
    {
        if (SelectedMember == null)
        {
            MessageBox.Show("None selected", "Error", MessageBoxButton.OK);
            return;
        }
        var member = _memberService.GetById(SelectedMember.JMBG);
        if (member.CardNumber == -1)
        {
            MessageBox.Show("Member dont have a card", "Error", MessageBoxButton.OK);
            return;
        }

        var membershipCard = _membershipCardService.GetById(member.CardNumber);
        if (membershipCard.Status != MembershipCardStatus.Active)
        {
            MessageBox.Show("Member card is not active", "Error", MessageBoxButton.OK);
            return;
        }

        var bookCount = 0;
        foreach (var card in _membershipService.GetAll())
        {
            if (card.Id == membershipCard.Membership.Id)
            {
                bookCount = card.MaxNumberOfBooks;
            }
        }
        if (_bookBorrowService.CountHoldingBooksByCardId(membershipCard.Id) >= bookCount)
        {
            MessageBox.Show("Member already has maximum number of books!", "Error", MessageBoxButton.OK);
            return;
        }
        
        var window = new BorrowBookView()
        {
            DataContext = new BorrowBookViewModel(_titleService, _bookBorrowService, membershipCard)
        };
        window.Show();
    }
}