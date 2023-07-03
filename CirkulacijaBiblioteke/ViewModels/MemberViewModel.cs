using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class MemberViewModel : ViewModelBase
{
    private Member _member;

    public String FirstName => _member.Name;
    public String LastName => _member.LastName;
    public String JMBG => _member.JMBG;


    public MemberViewModel(Member member)
    {
        _member = member;
    }

    public override string ToString()
    {
        return $"First name : {FirstName}, Last name: {LastName}, JMBG: {JMBG}";
    }
}