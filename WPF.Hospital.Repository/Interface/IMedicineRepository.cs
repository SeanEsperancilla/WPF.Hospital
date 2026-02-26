using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public interface IMedicineRepository
    {
        List<Medicine> GetAll();
        Medicine? Get(int id);
        void Add(Medicine entity);
        void Update(Medicine entity);
        void Delete(int id);
        int Save();
    }
}
