using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.Utilities;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels
{
    public class ReturnBookViewModel : ViewModelBase
    {
        private BookBorrowService _bookBorrowService;
        private TitleService _titleService;
        private PaymentService _paymentService;
        private MembershipCardService _membershipCardService;
        private ObservableCollection<BorrowedBooksViewModel> _borrowedBooks;
        private ObservableCollection<BorrowedBooksViewModel> _allBorrowedBooks;
        private ObservableCollection<BorrowedBooksViewModel> _filteredBorrowedBooks;
        private string _searchText = "";

        public ICommand ReturnBookCommand { get; }

        public ReturnBookViewModel(BookBorrowService bookBorrowService, TitleService titleService, PaymentService paymentService, MembershipCardService membershipCardService)
        {
            _bookBorrowService = bookBorrowService;
            _titleService = titleService;
            _paymentService = paymentService;
            _membershipCardService = membershipCardService;
            _bookBorrowService.DataChanged += (sender, args) => UpdateTable();
            _allBorrowedBooks = new ObservableCollection<BorrowedBooksViewModel>(
                _bookBorrowService.GetAll()
                    .Where(bookBorrow => !bookBorrow.Returned)
                    .Select(bookBorrow => new BorrowedBooksViewModel(bookBorrow, _bookBorrowService))
            );
            _borrowedBooks = _allBorrowedBooks;

            ReturnBookCommand = new DelegateCommand(o => ReturnBook());
        }

        public BorrowedBooksViewModel SelectedBookBorrow { get; set; }

        public string SearchBox
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    UpdateTable();
                    OnPropertyChanged();
                }
            }
        }

        public IEnumerable<BorrowedBooksViewModel> BookBorrows
        {
            get => _borrowedBooks;
            set
            {
                _borrowedBooks = new ObservableCollection<BorrowedBooksViewModel>(value);
                OnPropertyChanged();
            }
        }

        private void UpdateTable()
        {
            _allBorrowedBooks = new ObservableCollection<BorrowedBooksViewModel>(
                _bookBorrowService.GetAll()
                    .Where(bookBorrow => !bookBorrow.Returned)
                    .Select(bookBorrow => new BorrowedBooksViewModel(bookBorrow, _bookBorrowService))
            );
            _filteredBorrowedBooks = _allBorrowedBooks;
            var aux = _filteredBorrowedBooks.Intersect(UpdateTableFromSearch());
            BookBorrows = aux;
        }

        private ObservableCollection<BorrowedBooksViewModel> UpdateTableFromSearch()
        {
            if (!string.IsNullOrEmpty(_searchText))
                return new ObservableCollection<BorrowedBooksViewModel>(Search(_searchText));
            return _allBorrowedBooks;
        }

        public IEnumerable<BorrowedBooksViewModel> Search(string inputText)
        {
            return _allBorrowedBooks.Where(item => item.ToString().Contains(inputText));
        }

        private bool _isBookDamaged;

        public bool IsBookDamaged
        {
            get => _isBookDamaged;
            set
            {
                _isBookDamaged = value;
                OnPropertyChanged();
            }
        }

        private void ReturnBook()
        {
            if (SelectedBookBorrow == null)
            {
                MessageBox.Show("None selected", "Error", MessageBoxButton.OK);
                return;
            }

            var borrowedBook = _bookBorrowService.GetById(SelectedBookBorrow.Id);
            var membersCard = borrowedBook.MembershipCard;

            if (IsBookDamaged)
            {
                borrowedBook.Copy.State = Copy.InstanceState.Damaged;
                

                var payingRepairs = MessageBox.Show("Has member payed for damaging book?", "Warning", MessageBoxButton.YesNo);
                if (payingRepairs == MessageBoxResult.Yes)
                {
                    var newFine = new Fine(DateTime.Now, true);
                    membersCard.Fines.Add(newFine);
                    _membershipCardService.Update(membersCard);
                    
                    var payment = new Payment(IDGenerator.GetId(), borrowedBook.Copy.Price, PaymentType.DamagingBook,
                        borrowedBook.MembershipCard);
                    _paymentService.AddPayment(payment);
                }
                else
                {
                    MessageBox.Show("Member must pay for damaging book before returning it", "Error",
                        MessageBoxButton.OK);
                    return;
                }
            }
            else
            {
                borrowedBook.Copy.State = Copy.InstanceState.Available;
            }

            // Checking if member returned book after crossing deadline 
            if (borrowedBook.CreationDate.AddDays(membersCard.Membership.MembershipExtensionDeadline) < DateTime.Today)
            {
                var payingDeadlineExtension = MessageBox.Show("Has member paid for not returning book on time?", "Warning", MessageBoxButton.YesNo);
                if (payingDeadlineExtension == MessageBoxResult.Yes)
                {
                    var newFine = new Fine(DateTime.Now, true);
                    membersCard.Fines.Add(newFine);
                    _membershipCardService.Update(membersCard);
                    
                    var payment = new Payment(IDGenerator.GetId(), borrowedBook.Copy.Price, PaymentType.DelayedReturn,
                        borrowedBook.MembershipCard);
                    _paymentService.AddPayment(payment);   
                }
                else
                {
                    MessageBox.Show("Member must pay for delaying return before returning book", "Error",
                        MessageBoxButton.OK);
                    return;   
                }
            }
            
            _titleService.UpdateCopy(borrowedBook.Copy.TitleIsbn, borrowedBook.Copy);
            borrowedBook.Returned = true;
            _bookBorrowService.Update(borrowedBook.Id, borrowedBook);
            
            MessageBox.Show("Book was successfully returned", "Notification", MessageBoxButton.OK);
            UpdateTable();
        }
    }
}
