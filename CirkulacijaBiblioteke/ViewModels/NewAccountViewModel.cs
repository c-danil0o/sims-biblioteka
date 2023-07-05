using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Reflection.Metadata.BlobBuilder;

namespace CirkulacijaBiblioteke.ViewModels
{
    public class NewAccountViewModel : ViewModelBase
    {
        private MemberService _memberService;
        private UserAccountService _userAccountService;
        private string? _emailBox;
        private string? _passwordBox;
        private string? _nameBox;
        private string? _lastNameBox;
        private string? _jmbgBox;
        private string? _phoneBox;
        private string? _cityBox;
        private string? _streetBox;
        private string? _numberBox;

        public string EmailBox
        {
            get => _emailBox;
            set
            {
                if (value != _emailBox)
                {
                    _emailBox = value;
                    OnPropertyChanged("EmailBox");
                }
            }
        }
        public string PasswordBox
        {
            get => _passwordBox;
            set
            {
                if (value != _passwordBox)
                {
                    _passwordBox = value;
                    OnPropertyChanged("PasswordBox");
                }
            }
        }

        public string NameBox
        {
            get => _nameBox;
            set
            {
                if (value != _nameBox)
                {
                    _nameBox = value;
                    OnPropertyChanged("NameBox");
                }
            }
        }

        public string LastNameBox
        {
            get => _lastNameBox;
            set
            {
                if (value != _lastNameBox)
                {
                    _lastNameBox = value;
                    OnPropertyChanged("LastNameBox");
                }
            }
        }

        public string JMBGBox
        {
            get => _jmbgBox;
            set
            {
                if (value != _jmbgBox)
                {
                    _jmbgBox = value;
                    OnPropertyChanged("JMBGBox");
                }
            }
        }

        public string PhoneBox
        {
            get => _phoneBox;
            set
            {
                if (value != _phoneBox)
                {
                    _phoneBox = value;
                    OnPropertyChanged("PhoneBox");
                }
            }
        }

        public string CityBox
        {
            get => _cityBox;
            set
            {
                if (value != _cityBox)
                {
                    _cityBox = value;
                    OnPropertyChanged("CityBox");
                }
            }
        }

        public string StreetBox
        {
            get => _streetBox;
            set
            {
                if (value != _streetBox)
                {
                    _streetBox = value;
                    OnPropertyChanged("StreetBox");
                }
            }
        }

        public string NumberBox
        {
            get => _numberBox;
            set
            {
                if (value != _numberBox)
                {
                    _numberBox = value;
                    OnPropertyChanged("NumberBox");
                }
            }
        }

        public NewAccountViewModel(MemberService memberService, UserAccountService userAccountService)
        {
            _memberService = memberService;
            RegisterCommand = new DelegateCommand(o => Register(), o => CanRegister());
            _userAccountService = userAccountService;
        }
        public ICommand RegisterCommand { get; private set; }

        public bool ValidateRegistrationInfo()
        {
            Regex emailRegex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");

            if (_memberService.GetById(JMBGBox) != null)
            {
                MessageBox.Show("Existing jmbg", "Error", MessageBoxButton.OK);
                return false;
            }
            if (!emailRegex.IsMatch(EmailBox))
            {
                MessageBox.Show("Invalid email format", "Error", MessageBoxButton.OK);
                return false;
            }
            if (!JMBGBox.All(char.IsDigit))
            {
                MessageBox.Show("JMBG must contain only digits", "Error", MessageBoxButton.OK);
                return false;
            }
            if (!Regex.IsMatch(NameBox, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Name must contain only letters", "Error", MessageBoxButton.OK);
                return false;
            }
            if (!Regex.IsMatch(LastNameBox, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Last name must contain only letters", "Error", MessageBoxButton.OK);
                return false;
            }
            if (!PhoneBox.All(char.IsDigit))
            {
                MessageBox.Show("Phone number must contain only digits", "Error", MessageBoxButton.OK);
                return false;
            }
            return true;
        }
        private void Register()
        {
            if (ValidateRegistrationInfo())
            {
                UserAccount account = new UserAccount(EmailBox, PasswordBox, UserAccount.AccountType.Member);
                _userAccountService.AddUser(account);
                _memberService.AddMember(new Models.Member(JMBGBox, NameBox, LastNameBox, PhoneBox, account,
                    new Address(CityBox, StreetBox, NumberBox)));

                MessageBox.Show("Account created successfully", "Success", MessageBoxButton.OK);
                ClearFields();
            }
        }

        public void ClearFields()
        {
            EmailBox = String.Empty;
            PasswordBox = String.Empty;
            JMBGBox = String.Empty;
            NameBox = String.Empty;
            LastNameBox = String.Empty;
            PhoneBox = String.Empty;
            CityBox = String.Empty;
            StreetBox = String.Empty;
            NumberBox = String.Empty;
        }

        public bool CanRegister()
        {
            return !string.IsNullOrEmpty(EmailBox) && !string.IsNullOrEmpty(PasswordBox) &&
            !string.IsNullOrEmpty(JMBGBox) && !string.IsNullOrEmpty(NameBox) &&
            !string.IsNullOrEmpty(LastNameBox) && !string.IsNullOrEmpty(PhoneBox) &&
            !string.IsNullOrEmpty(CityBox) && !string.IsNullOrEmpty(StreetBox) &&
            !string.IsNullOrEmpty(NumberBox);
        }

    }
}
