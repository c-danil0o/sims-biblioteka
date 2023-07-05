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
    public class PaymentRepository : ISerializable
    {
        private readonly string _fileName = @".\..\..\..\Data\payment.json";
        private List<Payment>? _payments;

        public PaymentRepository()
        {
            _payments = new List<Payment>();
            Serializer.Load(this);
        }

        public string FileName()
        {
            return _fileName;
        }

        public IEnumerable<object>? GetList()
        {
            return _payments;
        }

        public void Import(JToken token)
        {
            _payments = token.ToObject<List<Payment>>();
        }

        public IEnumerable<Payment> GetAll()
        {
            return _payments;
        }

        public void Insert(Payment payment)
        {
            _payments?.Add(payment);
            Serializer.Save(this);
        }

        public void Delete(Payment entity)
        {
            _payments.Remove(entity);
            Serializer.Save(this);
        }

        public Payment GetById(int id)
        {
            return _payments.FirstOrDefault(py => py.Id == id);
        }
    }
}
