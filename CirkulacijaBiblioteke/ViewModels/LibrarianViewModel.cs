using System.Windows.Input;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class LibrarianViewModel : ViewModelBase
{
    private object _currentView;
    private MemberService _memberService;
    public LibrarianViewModel(MemberService memberService)
    {
        _memberService = memberService;
        _currentView = new NewAccountViewModel(memberService);

        NewAccountCommand = new DelegateCommand(o => NewAccountView());
        NewMemberCardCommand= new DelegateCommand(o => NewMemberCardView());

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
        
    }

    private void NewAccountView()
    {
        CurrentView = new NewAccountViewModel(_memberService);
    }


}