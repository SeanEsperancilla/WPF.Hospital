using System;
using System.Collections.Generic;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicineService(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public (bool Ok, string Message) Add(Medicine medicine)
        {
            if (medicine == null)
                return (false, "Medicine cannot be null.");

            if (string.IsNullOrWhiteSpace(medicine.Name))
                return (false, "Medicine name is required.");

            var existing = _medicineRepository.Get(medicine.Id);
            if (existing != null)
                return (false, "Medicine already exists.");

            _medicineRepository.Add(medicine);
            return (true, "Medicine added successfully.");
        }

        public (bool Ok, string Message) Delete(int id)
        {
            var existing = _medicineRepository.Get(id);
            if (existing == null)
                return (false, "Medicine not found.");

            _medicineRepository.Delete(id);
            return (true, "Medicine deleted successfully.");
        }

        public Medicine? Get(int id)
        {
            if (id <= 0)
                return null;

            return _medicineRepository.Get(id);
        }

        public List<Medicine> GetAll()
        {
            return _medicineRepository.GetAll();
        }

        public (bool Ok, string Message) Update(Medicine medicine)
        {
            if (medicine == null)
                return (false, "Medicine cannot be null.");

            var existing = _medicineRepository.Get(medicine.Id);
            if (existing == null)
                return (false, "Medicine not found.");

            existing.Id = medicine.Id;
            existing.Name = medicine.Name;
            existing.Brand = medicine.Brand;

            _medicineRepository.Update(existing);

            return (true, "Medicine updated successfully.");
        }
    }
}