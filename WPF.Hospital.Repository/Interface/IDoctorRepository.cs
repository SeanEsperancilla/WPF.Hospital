using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public interface IDoctorRepository
    {
        List<Doctor> GetAll();
        Doctor? Get(int id);
        void Add(Doctor entity);
        void Update(Doctor entity);
        void Delete(int id);
        int Save();
    }
}
