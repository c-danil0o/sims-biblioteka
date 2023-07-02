using CirkulacijaBiblioteke.Models;
using CirkulacijaBiblioteke.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirkulacijaBiblioteke.Services
{
    public class PaymentService
    {
        private PaymentRepository _paymentRepository;

        public PaymentService(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public List<Payment>? GetAll()
        {
            return _paymentRepository.GetAll() as List<Payment>;
        }

        public void AddMember(Payment payment)
        {
            _paymentRepository.Insert(payment);
            DataChanged?.Invoke(this, new EventArgs());
        }

        public void Update(int id, Payment payment)
        {
            var oldMember = _paymentRepository.GetById(id);
            if (oldMember == null)
            {
                throw new KeyNotFoundException();
            }
            _paymentRepository.Delete(oldMember);
            _paymentRepository.Insert(payment);
            DataChanged?.Invoke(this, new EventArgs());
        }

        public void Delete(int id)
        {
            _paymentRepository.Delete(_paymentRepository.GetById(id));
            DataChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler? DataChanged;
    }
}
