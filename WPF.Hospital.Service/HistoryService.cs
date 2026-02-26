using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.DTO;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }
        public (bool Ok, string Message) Create(History history)
        {
            throw new NotImplementedException();
        }

        public (bool Ok, string Message) Delete(int id)
        {
            throw new NotImplementedException();
        }

        public History? Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<History> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<History> GetByPatient(int patientId)
        {
            throw new NotImplementedException();
        }

        public (bool Ok, string Message) Update(History history)
        {
            throw new NotImplementedException();
        }
    }
}
