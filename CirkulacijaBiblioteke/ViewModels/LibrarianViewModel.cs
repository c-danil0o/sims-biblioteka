using System.Windows.Input;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class LibrarianViewModel : ViewModelBase
{
    private object _currentView;

    public LibrarianViewModel()
    {
        //_currentView =  DEFAULTNI viewmodel

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
        CurrentView = new NewAccountView();
    }


}