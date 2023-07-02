using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Repositories
{
    public class BookBorrowRepository : ISerializable
    {
        private readonly string _fileName = @".\..\..\..\Data\bookBorrow.json";
        private List<BookBorrow>? _bookBorrows;

        public BookBorrowRepository()

        {
            _bookBorrows = new List<BookBorrow>();
            Serializer.Load(this);
        }

        public string FileName()
        {
            return _fileName;
        }

        public IEnumerable<object>? GetList()
        {
            return _bookBorrows;
        }

        public void Import(JToken token)
        {
            _bookBorrows = token.ToObject<List<BookBorrow>>();
        }

        public IEnumerable<BookBorrow> GetAll()
        {
            return _bookBorrows;
        }

        public void Insert(BookBorrow newBookBorrow)
        {
            _bookBorrows?.Add(newBookBorrow);
            Serializer.Save(this);
        }

        public void Delete(BookBorrow entity)
        {
            _bookBorrows.Remove(entity);
            Serializer.Save(this);
        }

        public BookBorrow GetById(int id)
        {
            return _bookBorrows.FirstOrDefault(bb => bb.Id == id);
        }
    }
}
