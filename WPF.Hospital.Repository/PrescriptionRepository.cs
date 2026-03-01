using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly HospitalDbContext _context;
        public PrescriptionRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public void Add(Prescription entity)
        {
            _context.Prescription.Add(entity);
        }

        public void Delete(int id)
        {
            var prescription = _context.Prescription.Find(id);
            if (prescription != null)
            {
                _context.Prescription.Remove(prescription);
            }
        }

        public Prescription? Get(int id)
        {
            return _context.Prescription.Find(id);
        }

        public List<Prescription> GetAll()
        {
            return _context.Prescription.ToList();
        }

        public List<Prescription> GetByHistory(int historyId)
        {
            return _context.Prescription.Where(p => p.HistoryId == historyId).ToList();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(Prescription entity)
        {
            _context.Prescription.Update(entity);
        }

        public List<Prescription> GetByPatientId(int patientId)
        {
            return _context.Prescription
                           .Where(p => p.History.PatientId == patientId)
                           .ToList();
        }


        IEnumerable<Prescription> IPrescriptionRepository.GetByPatientId(int patientId)
        {
            return GetByPatientId(patientId);
        }
    }
}
