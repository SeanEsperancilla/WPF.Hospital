using System;
using System.Collections.Generic;
using System.Linq;
using WPF.Hospital.DTO;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IHistoryRepository _historyRepository;

        public DoctorService(IDoctorRepository doctorRepository,
                             IHistoryRepository historyRepository)
        {
            _doctorRepository = doctorRepository;
            _historyRepository = historyRepository;
        }


        public (bool Ok, string Message) Add(Doctor doctor)
        {
            if (doctor == null)
                return (false, "Doctor cannot be null.");

            if (string.IsNullOrWhiteSpace(doctor.FirstName) || string.IsNullOrWhiteSpace(doctor.LastName))
                return (false, "Doctor name is required.");

            var existing = _doctorRepository.Get(doctor.Id);
            if (existing != null)
                return (false, "Doctor already exists.");

            var modelDoctor = new WPF.Hospital.Model.Doctor
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName
            };

            _doctorRepository.Add(modelDoctor);
            return (true, "Doctor added successfully.");

        }

        public (bool Ok, string Message) Delete(int id)
        {
            var doctor = _doctorRepository.Get(id);
            if (doctor == null) return (false, "Doctor not found.");

            var histories = _historyRepository.GetAll()
                .Where(h => h.DoctorId == id).ToList();

            if (histories.Any())
                return (false, "Cannot delete doctor because related histories exist.");

            _doctorRepository.Delete(id);
            _doctorRepository.Save();
            return (true, "Doctor deleted successfully.");
        }


        public Doctor? Get(int id)
        {
            var modelDoctor = _doctorRepository.Get(id);
            if (modelDoctor == null)
                return null;

            return new Doctor
            {
                Id = modelDoctor.Id,
                FirstName = modelDoctor.FirstName,
                LastName = modelDoctor.LastName
            };
        }

        public List<Doctor> GetAll()
        {
            var modelDoctors = _doctorRepository.GetAll();
            if (modelDoctors == null)
                return new List<Doctor>();

            return modelDoctors.Select(md => new Doctor
            {
                Id = md.Id,
                FirstName = md.FirstName,
                LastName = md.LastName
            }).ToList();
        }

        public (bool Ok, string Message) Update(Doctor doctor)
        {
            if (doctor == null)
                return (false, "Doctor cannot be null.");

            var existing = _doctorRepository.Get(doctor.Id);
            if (existing == null)
                return (false, "Doctor not found.");

            var modelDoctor = new WPF.Hospital.Model.Doctor
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName
            };

            _doctorRepository.Update(modelDoctor);
            return (true, "Doctor updated successfully.");
        }

        public void Save()
        {
            _doctorRepository.Save();
        }
    }
}