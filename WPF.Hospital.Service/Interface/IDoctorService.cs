using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;

namespace WPF.Hospital.Service
{
    public interface IDoctorService
    {
        List<Doctor> GetAll();
        Doctor? Get(int id);
        (bool Ok, string Message) Add(Doctor doctor);
        (bool Ok, string Message) Update(Doctor doctor);
        (bool Ok, string Message) Delete(int id);
    }
}
