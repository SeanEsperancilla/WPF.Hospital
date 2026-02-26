using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public (bool Ok, string Message) Add(Doctor doctor)
        {
            if (doctor == null)
                return (false, "Doctor cannot be null.");

            if (string.IsNullOrWhiteSpace(doctor.FirstName) ||
                string.IsNullOrWhiteSpace(doctor.LastName))
                return (false, "Doctor name is required.");

            _doctorRepository.Add(doctor);
            return (true, "Doctor created successfully.");
        }

        public (bool Ok, string Message) Delete(int id)
        {
            var doctor = _doctorRepository.Get(id);
            if (doctor == null)
                return (false, "Doctor not found.");

            _doctorRepository.Delete(id);
            return (true, "Doctor deleted successfully.");
        }

        public Doctor? Get(int id)
        {
            return _doctorRepository.Get(id);
        }

        public List<Doctor> GetAll()
        {
            return _doctorRepository.GetAll();
        }

        public (bool Ok, string Message) Update(Doctor doctor)
        {
            if (doctor == null)
                return (false, "Doctor cannot be null.");

            var existing = _doctorRepository.Get(doctor.Id);
            if (existing == null)
                return (false, "Doctor not found.");

            _doctorRepository.Update(doctor);
            return (true, "Doctor updated successfully.");
        }
    }
}
