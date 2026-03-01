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
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PatientService(IPatientRepository repository,
                              IHistoryRepository historyRepository,
                              IPrescriptionRepository prescriptionRepository)
        {
            _patientRepository = repository;
            _historyRepository = historyRepository;
            _prescriptionRepository = prescriptionRepository;
        }

        public Patient Get(int id)
        {
            var patient = _patientRepository.Get(id);

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
                .Select(patient => new Patient
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Age = patient.Age,
                    Birthdate = patient.Birthdate,
                });
        }

        public void Add(Patient patient)
        {
            _patientRepository.Add(new Model.Patient
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Birthdate = patient.Birthdate,
            });
        }

        public void Update(Patient patient)
        {
            _patientRepository.Update(new Model.Patient
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Birthdate = patient.Birthdate,
            });
        }

        public void Delete(int id)
        {
            var histories = _historyRepository.GetByPatientId(id);
            if (histories.Any())
            {
                throw new InvalidOperationException("Cannot delete patient because related medical history records exist.");
            }

            var prescriptions = _prescriptionRepository.GetByPatientId(id);
            if (prescriptions.Any())
            {
                throw new InvalidOperationException("Cannot delete patient because related prescription records exist.");
            }

            _patientRepository.Delete(id);
        }


        public void Save()
        {
            _patientRepository.Save();
        }
    }

}
