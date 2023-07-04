using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Services;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class MemberViewModel : ViewModelBase
{
    private Member _member;
    private MembershipCardService _membershipCardService;

    public String FirstName => _member.Name;
    public String LastName => _member.LastName;
    public String JMBG => _member.JMBG;
    public String Type { get; set; } 
    public String Expired { get; set; }
    public String ExpirationDate { get; set; }


    public MemberViewModel(Member member, MembershipCardService membershipCardService)
    {
        _member = member;
        _membershipCardService = membershipCardService;
        var card = _membershipCardService.GetById(member.CardNumber);
        if (card == null)
        {
            Type = "None";
            Expired = "";
            ExpirationDate = "";
        }
        else
        {
            Type = card.Membership.Type.ToString();
            Expired = card.ValidUntil < DateTime.Now ? "Yes" : "No";
            ExpirationDate = card.ValidUntil.ToShortDateString();
        }
    }

    public override string ToString()
    {
        return $"First name : {FirstName}, Last name: {LastName}, JMBG: {JMBG}";
    }
}