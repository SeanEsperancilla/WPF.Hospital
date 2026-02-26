using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;

namespace WPF.Hospital.Service
{
    public interface IMedicineService
    {
        List<Medicine> GetAll();
        Medicine? Get(int id);
        (bool Ok, string Message) Create(Medicine medicine);
        (bool Ok, string Message) Update(Medicine medicine);
        (bool Ok, string Message) Delete(int id);
    }
}
