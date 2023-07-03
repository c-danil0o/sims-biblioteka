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
using CirkulacijaBiblioteke.Utilities;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class CreateMemberCardViewModel : ViewModelBase
{
    private MembershipService _membershipService;
    private MembershipCardService _membershipCardService;
    private MemberService _memberService;
    private ObservableCollection<MembershipViewModel> _memberships;
    private Member _member;
    public ICommand CreateMemberCardCommand { get; set; }
    public CreateMemberCardViewModel(MembershipService membershipService,MembershipCardService membershipCardService,MemberService memberService, string jmbg)
    {
        _membershipService = membershipService;
        _membershipCardService = membershipCardService;
        _memberService = memberService;
        _memberships = new ObservableCollection<MembershipViewModel>();
        foreach (var membership in _membershipService.GetAll())
        {
            _memberships.Add(new MembershipViewModel(membership));
        }
        _member = _memberService.GetById(jmbg);
        CreateMemberCardCommand = new DelegateCommand(o => CreateCard());
    }
    
    public MembershipViewModel SelectedMembership { get; set; }
    public String FirstName => _member.Name;
    public String LastName => _member.LastName;

    public IEnumerable<MembershipViewModel> Memberships
    {
        get => _memberships;
        set
        {
            _memberships = new ObservableCollection<MembershipViewModel>(value);
            OnPropertyChanged();
        }
    }

    private void CreateCard()
    {
        if (SelectedMembership == null)
        {
            MessageBox.Show("None selected", "Error", MessageBoxButton.OK);
            return;
        }

        var membership = _membershipService.GetByType(SelectedMembership.Type);
        MembershipCard card = new MembershipCard(membership, IDGenerator.GetId(), DateTime.Today.AddMonths(6),
            MembershipCardStatus.Active);
        _membershipCardService.AddMembershipCard(card);
        _member.CardNumber = card.Id;
        _memberService.Update(_member);
        MessageBox.Show("Member Card created successfully", "Success", MessageBoxButton.OK);
    }
}