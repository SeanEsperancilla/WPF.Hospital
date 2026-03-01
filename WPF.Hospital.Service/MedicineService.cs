using System;
using System.Collections.Generic;
using WPF.Hospital.DTO;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;

        public MedicineService(IMedicineRepository medicineRepository,
                               IPrescriptionRepository prescriptionRepository)
        {
            _medicineRepository = medicineRepository;
            _prescriptionRepository = prescriptionRepository;
        }

        public (bool Ok, string Message) Add(Medicine medicine)
        {
            if (medicine == null) return (false, "Medicine cannot be null.");
            if (string.IsNullOrWhiteSpace(medicine.Name)) return (false, "Medicine name is required.");

            var existing = _medicineRepository.Get(medicine.Id);
            if (existing != null) return (false, "Medicine already exists.");

            var modelMedicine = new WPF.Hospital.Model.Medicine
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Brand = medicine.Brand
            };

            _medicineRepository.Add(modelMedicine);
            _medicineRepository.Save();

            return (true, "Medicine added successfully.");
        }

        public Medicine? Get(int id)
        {
            if (id <= 0) return null;

            var medicineModel = _medicineRepository.Get(id);
            if (medicineModel == null) return null;

            return new Medicine
            {
                Id = medicineModel.Id,
                Name = medicineModel.Name,
                Brand = medicineModel.Brand
            };
        }

        public List<Medicine> GetAll()
        {
            return _medicineRepository.GetAll()
                .Select(m => new Medicine
                {
                    Id = m.Id,
                    Name = m.Name,
                    Brand = m.Brand
                })
                .ToList();
        }

        public (bool Ok, string Message) Update(Medicine medicine)
        {
            if (medicine == null) return (false, "Medicine cannot be null.");

            var existing = _medicineRepository.Get(medicine.Id);
            if (existing == null) return (false, "Medicine not found.");

            existing.Name = medicine.Name;
            existing.Brand = medicine.Brand;

            _medicineRepository.Update(existing);
            _medicineRepository.Save();

            return (true, "Medicine updated successfully.");
        }

        public (bool Ok, string Message) Delete(int id)
        {
            var medicine = _medicineRepository.Get(id);
            if (medicine == null) return (false, "Medicine not found.");

            var prescriptions = _prescriptionRepository.GetAll()
                .Where(p => p.MedicineId == id).ToList();

            if (prescriptions.Any())
                return (false, "Cannot delete medicine because related prescriptions exist.");

            _medicineRepository.Delete(id);
            _medicineRepository.Save();

            return (true, "Medicine deleted successfully.");
        }

        public void Save()
        {
            _medicineRepository.Save();
        }
    }
}