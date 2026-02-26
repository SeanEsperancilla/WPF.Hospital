using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public interface IPrescriptionRepository
    {
        List<Prescription> GetAll();
        List<Prescription> GetByHistory(int historyId);
        Prescription? Get(int id);
        void Add(Prescription entity);
        void Update(Prescription entity);
        void Delete(int id);
        int Save();
    }
}
