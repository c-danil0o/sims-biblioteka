using System.Windows.Input;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class LibrarianViewModel : ViewModelBase
{
    private object _currentView;
    private MemberService _memberService;
    private UserAccountService _userAccountService;
    private MembershipService _membershipService;
    private MembershipCardService _membershipCardService;
    public LibrarianViewModel(MemberService memberService, UserAccountService userAccountService, MembershipService membershipService,MembershipCardService membershipCardService)
    {
        _memberService = memberService;
        _userAccountService = userAccountService;
        _membershipService = membershipService;
        _membershipCardService = membershipCardService;

        _currentView = new NewAccountViewModel(memberService, userAccountService);

        NewAccountCommand = new DelegateCommand(o => NewAccountView());
        NewMemberCardCommand = new DelegateCommand(o => NewMemberCardView());
    }

    public ICommand NewAccountCommand { get; private set; }
    public ICommand NewMemberCardCommand { get; private set; }
    

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    private void NewMemberCardView()
    {
        CurrentView = new NewMemberCardViewModel(_memberService, _membershipService, _membershipCardService);
    }

    private void NewAccountView()
    {
        CurrentView = new NewAccountViewModel(_memberService, _userAccountService);
    }


}