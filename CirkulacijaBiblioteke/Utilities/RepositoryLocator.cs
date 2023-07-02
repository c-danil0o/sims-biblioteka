using System.Diagnostics;
using CirkulacijaBiblioteke.Repositories;

namespace CirkulacijaBiblioteke.Utilities;

public class RepositoryLocator
{
    private MemberRepository _memberRepository;
    private UserAccountRepository _userAccountRepository;
    private TitleRepository _titleRepository;
    public RepositoryLocator()
    {
        _memberRepository = new MemberRepository();
        _userAccountRepository = new UserAccountRepository();
        _titleRepository = new TitleRepository();
    }

    public MemberRepository MemberRepository => _memberRepository;

    public UserAccountRepository UserAccountRepository => _userAccountRepository;
}