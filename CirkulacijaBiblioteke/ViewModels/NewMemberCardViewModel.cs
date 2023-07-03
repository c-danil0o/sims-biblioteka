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
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;
using static System.Reflection.Metadata.BlobBuilder;

namespace CirkulacijaBiblioteke.ViewModels;

public class NewMemberCardViewModel : ViewModelBase
{
    private MemberService _memberService;
    private MembershipService _membershipService;
    private MembershipCardService _membershipCardService;
    private ObservableCollection<MemberViewModel> _allMembers;
    private ObservableCollection<MemberViewModel> _filteredMembers;
    private ObservableCollection<MemberViewModel> _members;
    private string _searchText = "";

    public ICommand CreateMemberCardCommand { get; set; }
    public ICommand ExtendMemberCardCommand { get; set; }
    public ICommand ChangeMemberCardCommand { get; set; }


    public NewMemberCardViewModel(MemberService memberService, MembershipService membershipService, MembershipCardService membershipCardService)
    {
        _memberService = memberService;
        _membershipService = membershipService;
        _membershipCardService = membershipCardService;
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
            MessageBox.Show("Card havent expired yet", "Error", MessageBoxButton.OK);
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


}