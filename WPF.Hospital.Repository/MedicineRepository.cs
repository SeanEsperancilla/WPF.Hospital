using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly HospitalDbContext _context;
        public MedicineRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public void Add(Medicine entity)
        {
            _context.Medicine.Add(entity);
        }

        public void Delete(int id)
        {
            var medicine = _context.Medicine.Find(id);
            if (medicine != null)
            {
                _context.Medicine.Remove(medicine);
            }
        }

        public Medicine? Get(int id)
        {
            return _context.Medicine.Find(id);
        }

        public List<Medicine> GetAll()
        {
            return _context.Medicine.ToList();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(Medicine entity)
        {
            _context.Medicine.Update(entity);
        }
    }
}
