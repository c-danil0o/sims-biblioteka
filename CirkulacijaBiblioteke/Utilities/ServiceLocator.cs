using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.ViewModels;

namespace CirkulacijaBiblioteke.Utilities;

public class ServiceLocator
{
    private RepositoryLocator _repositoryLocator;
    private UserAccountService _userAccountService;
    private MemberService _memberService;
    private BookBorrowService _bookBorrowService;
    private MembershipCardService _membershipCardService;
    private MembershipService _membershipService;
    private PaymentService _paymentService;
    private TitleService _titleService;
    public ServiceLocator(RepositoryLocator repositoryLocator)
    {
        _repositoryLocator = repositoryLocator;
        _userAccountService = new UserAccountService(repositoryLocator.UserAccountRepository);
        _memberService = new MemberService(repositoryLocator.MemberRepository);
        _bookBorrowService = new BookBorrowService(repositoryLocator.BookBorrowRepository);
        _membershipCardService = new MembershipCardService(repositoryLocator.MembershipCardRepository);
        _membershipService = new MembershipService(repositoryLocator.MembershipRepository);
        _paymentService = new PaymentService(repositoryLocator.PaymentRepository);
        _titleService = new TitleService(repositoryLocator.TitleRepository);

    }

    public TitleService TitleService => _titleService;

    public BookBorrowService BookBorrowService => _bookBorrowService;

    public MembershipCardService MembershipCardService => _membershipCardService;

    public MembershipService MembershipService => _membershipService;

    public PaymentService PaymentService => _paymentService;

    public RepositoryLocator RepositoryLocator => _repositoryLocator;

    public UserAccountService UserAccountService => _userAccountService;

    public MemberService MemberService => _memberService;
}