using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Services;

namespace CirkulacijaBiblioteke.Utilities;

public class ServiceLocator
{
    private RepositoryLocator _repositoryLocator;
    private UserAccountService _userAccountService;
    private MemberService _memberService;
    public ServiceLocator(RepositoryLocator repositoryLocator)
    {
        _repositoryLocator = repositoryLocator;
        _userAccountService = new UserAccountService(repositoryLocator.UserAccountRepository);
        _memberService = new MemberService(repositoryLocator.MemberRepository);
    }

    public RepositoryLocator RepositoryLocator => _repositoryLocator;

    public UserAccountService UserAccountService => _userAccountService;

    public MemberService MemberService => _memberService;
}