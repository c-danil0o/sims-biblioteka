using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Services
{
    public class BookBorrowService
    {
        private BookBorrowRepository _bookBorrowRepository;

        public BookBorrowService(BookBorrowRepository bookBorrowRepository)
        {
            _bookBorrowRepository = bookBorrowRepository;
        }
        public List<BookBorrow>? GetAll()
        {
            return _bookBorrowRepository.GetAll() as List<BookBorrow>;
        }

        public BookBorrow? GetById(int id)
        {
            return _bookBorrowRepository.GetById(id);
        }

        public void AddMember(BookBorrow bookBorrow)
        {
            _bookBorrowRepository.Insert(bookBorrow);
            DataChanged?.Invoke(this, new EventArgs());
        }

        public void Update(int id, BookBorrow bookBorrow)
        {
            var oldMember = _bookBorrowRepository.GetById(id);
            if (oldMember == null)
            {
                throw new KeyNotFoundException();
            }
            _bookBorrowRepository.Delete(oldMember);
            _bookBorrowRepository.Insert(bookBorrow);
            DataChanged?.Invoke(this, new EventArgs());


        }

        public void Delete(int id)
        {
            _bookBorrowRepository.Delete(_bookBorrowRepository.GetById(id));
            DataChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler? DataChanged;
    }
}
