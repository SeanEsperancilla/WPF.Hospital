using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }
        public (bool Ok, string Message) Create(Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public (bool Ok, string Message) Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Prescription? Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Prescription> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Prescription> GetByHistory(int historyId)
        {
            throw new NotImplementedException();
        }

        public (bool Ok, string Message) Update(Prescription prescription)
        {
            throw new NotImplementedException();
        }
    }
}
