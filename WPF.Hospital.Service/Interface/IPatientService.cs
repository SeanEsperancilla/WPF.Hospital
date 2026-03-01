using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.DTO;

namespace WPF.Hospital.Service
{
    public interface IPatientService
    {
        Patient Get(int id);
        IEnumerable<Patient> GetAll();
        void Add(Patient patient);
        void Update(Patient patient);
        void Delete(int id);
        void Save();
    }
}
