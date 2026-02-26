using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalDbContext _context;
        public DoctorRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public void Add(Doctor entity)
        {
            _context.Doctor.Add(entity);
        }

        public void Delete(int id)
        {
            var doctor = _context.Doctor.Find(id);
            if (doctor != null)
            {
                _context.Doctor.Remove(doctor);
            }
        }

        public Doctor? Get(int id)
        {
            return _context.Doctor.Find(id);
        }

        public List<Doctor> GetAll()
        {
            return _context.Doctor.ToList();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(Doctor entity)
        {
            _context.Doctor.Update(entity);
        }
    }
}
