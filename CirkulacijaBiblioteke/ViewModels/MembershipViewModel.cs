using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.View;

namespace CirkulacijaBiblioteke.ViewModels;

public class MembershipViewModel : ViewModelBase
{
    private Membership _membership;

    public MembershipType Type => _membership.Type;
    public float Price => _membership.Price;
    public int MaxBooks=> _membership.MaxNumberOfBooks;


    public MembershipViewModel(Membership membership)
    {
        _membership = membership;
    }

}