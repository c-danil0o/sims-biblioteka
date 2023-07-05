using System.Diagnostics;
using CirkulacijaBiblioteke.Repositories;

namespace CirkulacijaBiblioteke.Utilities;

public class RepositoryLocator
{
    private MemberRepository _memberRepository;
    private UserAccountRepository _userAccountRepository;
    private TitleRepository _titleRepository;
    private BookBorrowRepository _bookBorrowRepository;
    private BookReservationRepository _bookReservationRepository;
    private MembershipRepository _membershipRepository;
    private MembershipCardRepository _membershipCardRepository;
    private PaymentRepository _paymentRepository;
    
    public RepositoryLocator()
    {
        _memberRepository = new MemberRepository();
        _userAccountRepository = new UserAccountRepository();
        _titleRepository = new TitleRepository();
        _bookBorrowRepository = new BookBorrowRepository();
        _bookReservationRepository = new BookReservationRepository();
        _membershipRepository = new MembershipRepository();
        _membershipCardRepository = new MembershipCardRepository();
        _paymentRepository = new PaymentRepository();
    }

    public TitleRepository TitleRepository => _titleRepository;

    public BookBorrowRepository BookBorrowRepository => _bookBorrowRepository;

    public BookReservationRepository BookReservationRepository => _bookReservationRepository;

    public MembershipRepository MembershipRepository => _membershipRepository;

    public MembershipCardRepository MembershipCardRepository => _membershipCardRepository;

    public PaymentRepository PaymentRepository => _paymentRepository;


    public MemberRepository MemberRepository => _memberRepository;

    public UserAccountRepository UserAccountRepository => _userAccountRepository;
}