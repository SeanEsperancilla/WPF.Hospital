using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.DTO;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IHistoryRepository _historyRepository;

        public PatientService(IPatientRepository repository, IHistoryRepository historyRepository)
        {
            _patientRepository = repository;
            _historyRepository = historyRepository;
        }

        public Patient Get(int id)
        {
            Model.Patient patient = _patientRepository.Get(id);

            return new Patient
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Birthdate = patient.Birthdate,
                History = _historyRepository.GetByPatientId(id)
                    .Select(h => new History
                    {
                        Id = h.Id,
                        Procedure = h.Procedure,
                    })
            };
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patientRepository.GetAll()
                .Select(patient => new Patient()
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Age= patient.Age,
                    Birthdate = patient.Birthdate,
                });
        }
    }
}
