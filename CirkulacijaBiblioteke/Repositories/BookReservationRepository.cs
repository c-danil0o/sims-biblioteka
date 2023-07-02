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
    public class BookReservationRepository : ISerializable
    {
        private readonly string _fileName = @".\..\..\..\Data\bookReservation.json";
        private List<BookReservation>? _bookReservations;

        public BookReservationRepository()

        {
            _bookReservations = new List<BookReservation>();
            Serializer.Load(this);
        }

        public string FileName()
        {
            return _fileName;
        }

        public IEnumerable<object>? GetList()
        {
            return _bookReservations;
        }

        public void Import(JToken token)
        {
            _bookReservations = token.ToObject<List<BookReservation>>();
        }

        public IEnumerable<BookReservation> GetAll()
        {
            return _bookReservations;
        }

        public void Insert(BookReservation newBookReservation)
        {
            _bookReservations?.Add(newBookReservation);
            Serializer.Save(this);
        }

        public void Delete(BookReservation entity)
        {
            _bookReservations.Remove(entity);
            Serializer.Save(this);
        }

        public BookReservation GetById(int id)
        {
            return _bookReservations.FirstOrDefault(br => br.Id == id);
        }
    }
}
