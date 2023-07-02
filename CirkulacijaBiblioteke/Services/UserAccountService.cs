using System.Linq;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Repositories;

namespace CirkulacijaBiblioteke.Services;

public class UserAccountService
{
    private UserAccountRepository _userAccountRepository;

    public UserAccountService(UserAccountRepository userAccountRepository)
    {
        _userAccountRepository = userAccountRepository;
    }

    public void AddUser(UserAccount user)
    {
       _userAccountRepository.Insert(user);
    }

    public UserAccount? GetByEmail(string email)
    {
        return _userAccountRepository.GetByEmail(email);
    }

    public bool ValidateEmail(string email)
    {
        return _userAccountRepository.GetAll().FirstOrDefault(user => user.Email == email) != null;
    }
}