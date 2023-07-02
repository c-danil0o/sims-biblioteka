using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Repositories;

namespace CirkulacijaBiblioteke.Services;

public class TitleService
{
    private TitleRepository _titleRepository;

    public TitleService(TitleRepository titleRepository)
    {
        _titleRepository = titleRepository;
    }

    public List<Member>? GetAll()
    {
        return _titleRepository.GetAll() as List<Member>;
    }

    public Title? GetById(string jmbg)
    {
        return _titleRepository.GetById(jmbg);
    }

    public void AddMember(Title member)
    {
        _titleRepository.Insert(member);
        DataChanged?.Invoke(this, new EventArgs());
    }

    public void Update(string jmbg, Title title)
    {
        var oldMember = _titleRepository.GetById(jmbg);
        if (oldMember == null)
        {
            throw new KeyNotFoundException();
        }
        _titleRepository.Delete(oldMember);
        _titleRepository.Insert(title);
        DataChanged?.Invoke(this, new EventArgs());
    }

    public void Delete(string jmbg)
    {
        _titleRepository.Delete(_titleRepository.GetById(jmbg));
        DataChanged?.Invoke(this, new EventArgs());
    }



    public event EventHandler? DataChanged;
}