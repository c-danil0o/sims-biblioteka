using CirkulacijaBiblioteke.Repositories;

namespace CirkulacijaBiblioteke.Utilities;

public class RepositoryLocator
{
    private MemberRepository _memberRepository;
    private UserAccountRepository _userAccountRepository;
    public RepositoryLocator()
    {
        _memberRepository = new MemberRepository();
        _userAccountRepository = new UserAccountRepository();
    }

    public MemberRepository MemberRepository => _memberRepository;

    public UserAccountRepository UserAccountRepository => _userAccountRepository;
}