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

        public HistoryService(IPatientRepository patientRepository,
                              IHistoryRepository historyRepository)
        {
            _patientRepository = patientRepository;
            _historyRepository = historyRepository;
        }

        public (bool Ok, string Message) Add(History history)
        {
            if (history == null)
                return (false, "History cannot be null.");

            var patientId = history.Patient?.Id ?? 0;
            if (patientId <= 0)
                return (false, "Invalid patient reference.");

            var patientModel = _patientRepository.Get(patientId);
            if (patientModel == null)
                return (false, "Patient does not exist.");

            var model = MapDtoToModel(history);
            _historyRepository.Add(model);

            return (true, "History created successfully.");
        }

        public (bool Ok, string Message) Delete(int id)
        {
            var existing = _historyRepository.Get(id);
            if (existing == null)
                return (false, "History record not found.");

            _historyRepository.Delete(id);
            return (true, "History deleted successfully.");
        }

        public History? Get(int id)
        {
            var model = _historyRepository.Get(id);
            if (model == null)
                return null;

            return MapModelToDto(model);
        }

        public List<History> GetAll()
        {
            var models = _historyRepository.GetAll() ?? Enumerable.Empty<WPF.Hospital.Model.History>();
            return models.Select(MapModelToDto).ToList();
        }

        public List<History> GetByPatient(int patientId)
        {
            if (patientId <= 0)
                return new List<History>();

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
            if (patientId > 0)
            {
                var patientExists = _patientRepository.Get(patientId);
                if (patientExists == null)
                    return (false, "Patient does not exist.");
            }

            var model = MapDtoToModel(history);
            _historyRepository.Update(model);
            return (true, "History updated successfully.");
        }

        private static History MapModelToDto(WPF.Hospital.Model.History model)
        {
            if (model == null)
                return null!; 

            return new History
            {
                Id = model.Id,
                Procedure = model.Procedure,
                Patient = model.Patient == null ? null : new Patient
                {
                    Id = model.Patient.Id,
                    FirstName = model.Patient.FirstName,
                    LastName = model.Patient.LastName,
                    Age = model.Patient.Age,
                    Birthdate = model.Patient.Birthdate
                }
            };
        }

        private static WPF.Hospital.Model.History MapDtoToModel(History dto)
        {
            if (dto == null)
                return null!;

            var modelPatient = dto.Patient == null ? null : new WPF.Hospital.Model.Patient
            {
                Id = dto.Patient.Id,
                FirstName = dto.Patient.FirstName,
                LastName = dto.Patient.LastName,
                Age = dto.Patient.Age,
                Birthdate = dto.Patient.Birthdate
            };

            return new WPF.Hospital.Model.History
            {
                Id = dto.Id,
                Procedure = dto.Procedure,
                Patient = modelPatient
            };
        }
    }
}