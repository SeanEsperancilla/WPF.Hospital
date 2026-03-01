using System;
using System.Collections.Generic;
using WPF.Hospital.DTO;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class HistoryService : IHistoryService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IHistoryRepository _historyRepository;
        private readonly IDoctorRepository _doctorRepository;

        public HistoryService(IPatientRepository patientRepository,
                              IHistoryRepository historyRepository,
                              IDoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _historyRepository = historyRepository;
            _doctorRepository = doctorRepository;
        }

        public (bool Ok, string Message) Add(History history)
        {
            if (history == null)
                return (false, "History cannot be null.");

            var patientId = history.Patient?.Id ?? 0;
            if (patientId <= 0) return (false, "Invalid patient reference.");
            if (_patientRepository.Get(patientId) == null) return (false, "Patient does not exist.");

            var doctorId = history.Doctor?.Id ?? 0;
            if (doctorId <= 0) return (false, "Invalid doctor reference.");
            if (_doctorRepository.Get(doctorId) == null) return (false, "Doctor does not exist.");

            var model = MapDtoToModel(history);
            _historyRepository.Add(model);
            _historyRepository.Save();   

            return (true, "History created successfully.");
        }

        public (bool Ok, string Message) Delete(int id)
        {
            var existing = _historyRepository.Get(id);
            if (existing == null)
                return (false, "History record not found.");

            _historyRepository.Delete(id);
            _historyRepository.Save();   // ✅ Persist immediately

            return (true, "History deleted successfully.");
        }

        public History? Get(int id)
        {
            var model = _historyRepository.Get(id);
            return model == null ? null : MapModelToDto(model);
        }

        public List<History> GetAll()
        {
            var models = _historyRepository.GetAll() ?? Enumerable.Empty<WPF.Hospital.Model.History>();
            return models.Select(MapModelToDto).ToList();
        }

        public List<History> GetByPatient(int patientId)
        {
            if (patientId <= 0) return new List<History>();

            var models = _historyRepository.GetByPatientId(patientId) ?? Enumerable.Empty<WPF.Hospital.Model.History>();
            return models.Select(MapModelToDto).ToList();
        }

        public (bool Ok, string Message) Update(History history)
        {
            if (history == null)
                return (false, "History cannot be null.");

            var existing = _historyRepository.Get(history.Id);
            if (existing == null)
                return (false, "History record not found.");

            var patientId = history.Patient?.Id ?? 0;
            if (patientId > 0 && _patientRepository.Get(patientId) == null)
                return (false, "Patient does not exist.");

            var doctorId = history.Doctor?.Id ?? 0;
            if (doctorId > 0 && _doctorRepository.Get(doctorId) == null)
                return (false, "Doctor does not exist.");

            var model = MapDtoToModel(history);
            _historyRepository.Update(model);
            _historyRepository.Save();   // ✅ Persist immediately

            return (true, "History updated successfully.");
        }

        private static DTO.History MapModelToDto(WPF.Hospital.Model.History model)
        {
            if (model == null) return null!;

            return new DTO.History
            {
                Id = model.Id,
                Procedure = model.Procedure,
                Patient = model.Patient == null ? null : new DTO.Patient
                {
                    Id = model.Patient.Id,
                    FirstName = model.Patient.FirstName,
                    LastName = model.Patient.LastName,
                    Age = model.Patient.Age,
                    Birthdate = model.Patient.Birthdate
                },
                Doctor = model.Doctor == null ? null : new DTO.Doctor
                {
                    Id = model.Doctor.Id,
                    FirstName = model.Doctor.FirstName,
                    LastName = model.Doctor.LastName
                }
            };
        }


        private static WPF.Hospital.Model.History MapDtoToModel(DTO.History dto)
        {
            if (dto == null) return null!;

            var modelPatient = dto.Patient == null ? null : new WPF.Hospital.Model.Patient
            {
                Id = dto.Patient.Id,
                FirstName = dto.Patient.FirstName,
                LastName = dto.Patient.LastName,
                Age = dto.Patient.Age,
                Birthdate = dto.Patient.Birthdate
            };

            var modelDoctor = dto.Doctor == null ? null : new WPF.Hospital.Model.Doctor
            {
                Id = dto.Doctor.Id,
                FirstName = dto.Doctor.FirstName,
                LastName = dto.Doctor.LastName
            };

            return new WPF.Hospital.Model.History
            {
                Id = dto.Id,
                Procedure = dto.Procedure,
                Patient = modelPatient,
                Doctor = modelDoctor
            };
        }

        public void Save()
        {
            _historyRepository.Save();
        }

        public List<DTO.Patient> GetAllPatients()
        {
            var models = _patientRepository.GetAll() ?? new List<WPF.Hospital.Model.Patient>();
            return models.Select(p => new DTO.Patient
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                Birthdate = p.Birthdate
            }).ToList();
        }

        public List<DTO.Doctor> GetAllDoctors()
        {
            var models = _doctorRepository.GetAll() ?? new List<WPF.Hospital.Model.Doctor>();
            return models.Select(d => new DTO.Doctor
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName
            }).ToList();
        }
    }
}
