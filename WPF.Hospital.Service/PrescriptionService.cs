using System;
using System.Collections.Generic;
using WPF.Hospital.DTO;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Service
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        public (bool Ok, string Message) Add(Prescription prescription)
        {
            if (prescription == null)
                return (false, "Prescription cannot be null.");

            if (prescription.HistoryId <= 0)
                return (false, "Invalid history ID.");

            if (prescription.MedicineId <= 0)
                return (false, "Invalid medicine ID.");

            if (prescription.Quantity <= 0)
                return (false, "Quantity must be greater than zero.");

            if (string.IsNullOrWhiteSpace(prescription.Frequency))
                return (false, "Frequency is required.");

            _prescriptionRepository.Add(prescription);
            return (true, "Prescription added successfully.");
        }

        public (bool Ok, string Message) Delete(int id)
        {
            var existing = _prescriptionRepository.Get(id);
            if (existing == null)
                return (false, "Prescription not found.");

            _prescriptionRepository.Delete(id);
            return (true, "Prescription deleted successfully.");
        }

        public Prescription? Get(int id)
        {
            if (id <= 0)
                return null;

            return _prescriptionRepository.Get(id);
        }

        public List<Prescription> GetAll()
        {
            return _prescriptionRepository.GetAll();
        }

        public List<Prescription> GetByHistory(int historyId)
        {
            if (historyId <= 0)
                return new List<Prescription>();

            return _prescriptionRepository.GetByHistory(historyId);
        }

        public (bool Ok, string Message) Update(Prescription prescription)
        {
            if (prescription == null)
                return (false, "Prescription cannot be null.");

            var existing = _prescriptionRepository.Get(prescription.Id);
            if (existing == null)
                return (false, "Prescription not found.");

            existing.Id = prescription.Id;
            existing.HistoryId = prescription.HistoryId;
            existing.MedicineId = prescription.MedicineId;
            existing.Quantity = prescription.Quantity;
            existing.Frequency = prescription.Frequency ?? string.Empty;

            _prescriptionRepository.Update(existing);

            return (true, "Prescription updated successfully.");
        }
    }
}