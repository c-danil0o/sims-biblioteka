using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Repositories;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.Utilities;
using CirkulacijaBiblioteke.View.Archivist;
using CirkulacijaBiblioteke.View.Guest;
using CirkulacijaBiblioteke.View.Librarian;
using CirkulacijaBiblioteke.View.Member;
using CirkulacijaBiblioteke.ViewModels;

namespace CirkulacijaBiblioteke.View;

public partial class LoginDialog : Window, INotifyPropertyChanged
{
    private string? _email;
    private string? _password;
    private UserAccountService _userService;
    private ServiceLocator _serviceLocator;

    public LoginDialog(ServiceLocator serviceLocator)
    {
        InitializeComponent();
        _serviceLocator = serviceLocator;
        _userService = serviceLocator.UserAccountService;
        DataContext = this;
    }

    public string Email
    {
        get => _email;
        set
        {
            if (value != _email)
            {
                _email = value;
                OnPropertyChanged("EmailBox");
            }
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (value != _password)
            {
                _password = value;
                OnPropertyChanged("PasswordBox");
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        var user = GetLoggedUser();
        if (user == null)
            return;
        DialogResult = true;


        switch (user.Type)
        {
            case UserAccount.AccountType.Member:
                Application.Current.MainWindow = new MemberWindow
                    { DataContext = new MemberViewModel() };
                ;
                break;
            case UserAccount.AccountType.Archivist:
                Application.Current.MainWindow = new ArchivistWindow()
                    { DataContext = new ArchivistViewModel(_serviceLocator.TitleService) };
                ;
                break;
            case UserAccount.AccountType.Librarian:
                Application.Current.MainWindow = new LibrarianWindow
                {
                    DataContext = new LibrarianViewModel(_serviceLocator.MemberService)
                };
                

                break;
        }
    }

    private UserAccount? GetLoggedUser()
    {
        if (!_userService.ValidateEmail(Email))
        {
            MessageBox.Show("Invalid Email", "Error", MessageBoxButton.OK);
            return null;
        }

        var user = _userService.GetByEmail(Email);
        if (user != null && user.ValidatePassword(Password)) return user;
        MessageBox.Show("Invalid Password", "Error", MessageBoxButton.OK);
        return null;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        Password = PasswordBox.Password;
    }

    private void LoginAsGuestButton_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Application.Current.MainWindow = new GuestWindow
            { DataContext = new GuestViewModel(_serviceLocator.TitleService) };
        ;
    }
}