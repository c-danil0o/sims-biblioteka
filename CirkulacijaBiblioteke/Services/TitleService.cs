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

    public List<Title>? GetAll()
    {
        return _titleRepository.GetAll() as List<Title>;
    }

    public Title? GetById(string isbn)
    {
        return _titleRepository.GetById(isbn);
    }

    public void AddTitle(Title title)
    {
        _titleRepository.Insert(title);
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

    public void AddCopy(string isbn, Copy copy)
    {

        var book  = _titleRepository.GetById(isbn);
        if (book.Copies == null)
        {
            var copies = new List<Copy>();
            copies.Add(copy);
            book.Copies = copies;
        }
        else
        {
            book.Copies.Add(copy);
        }
        Update(isbn, book);
    }

    public void DeleteCopy(string isbn, int inventoryNumber)
    {
        var book = _titleRepository.GetById(isbn);
        book.Copies.RemoveAll(item => item.InventoryNumber == inventoryNumber);
        Update(isbn, book);
    }



    public event EventHandler? DataChanged;
}