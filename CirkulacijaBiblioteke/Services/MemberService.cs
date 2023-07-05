using System;
using System.Collections.Generic;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Repositories;

namespace CirkulacijaBiblioteke.Services;

public class MemberService
{
    private MemberRepository _memberRepository;

    public MemberService(MemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }
    public List<Member>? GetAll()
    {
        return _memberRepository.GetAll() as List<Member>;
    }

    public Member? GetById(string jmbg)
    {
        return _memberRepository.GetById(jmbg);
    }

    public void AddMember(Member member)
    {
        _memberRepository.Insert(member);
        DataChanged?.Invoke(this, new EventArgs());
    }

    public void Update(Member member)
    {
        var oldMember = _memberRepository.GetById(member.JMBG);
            if (oldMember == null)
            {
                throw new KeyNotFoundException();
            }
            _memberRepository.Delete(oldMember);
            _memberRepository.Insert(member);
            DataChanged?.Invoke(this, new EventArgs());
        

    }
    
    

    public void Delete(string jmbg)
    {
        _memberRepository.Delete(_memberRepository.GetById(jmbg));
        DataChanged?.Invoke(this, new EventArgs());
    }
    
    public event EventHandler? DataChanged;
}