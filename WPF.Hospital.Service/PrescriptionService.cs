using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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
            {
                return (false, "Prescription cannot be null.");
            }
            
            if (prescription.HistoryId <= 0)
            {
                return (false, "Invalid HistoryId.");
            }

            if (prescription.MedicineId <= 0)
            {
                return (false, "Invalid MedicineId.");
            }

            if (prescription.Quantity <= 0)
            {
                return (false, "Quantity must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(prescription.Frequency))
            {
                return (false, "Frequency cannot be empty.");
            }
            if (prescription.Frequency.Length > 100)
            {
                return (false, "Frequency cannot exceed 100 characters.");
            }

            var existing = _prescriptionRepository.Get(prescription.Id);
            if (existing == null)
                return (false, "Prescription already exist.");

            var modelPrescription = new WPF.Hospital.Model.Prescription
            {
                Id = prescription.Id,
                HistoryId = prescription.HistoryId,
                MedicineId = prescription.MedicineId,
                Quantity = prescription.Quantity,
                Frequency = prescription.Frequency
            };

            _prescriptionRepository.Add(modelPrescription);
            return (true, "Prescription added successfully.");
        }

        public (bool Ok, string Message) Delete(int id)
        {
            var existing = _prescriptionRepository.Get(id);
            if (existing == null)
                return (false, "Prescription not found.");

            _prescriptionRepository.Delete(id);
            return (true, "Prescription deleted succesfully");
        }

        public Prescription? Get(int id)
        {
            var modelPrescription = _prescriptionRepository.Get(id);
            if (modelPrescription == null)
                return null;

            return new Prescription
            {
                Id = modelPrescription.Id,
                HistoryId = modelPrescription.HistoryId,
                MedicineId = modelPrescription.MedicineId,
                Quantity = modelPrescription.Quantity,
                Frequency = modelPrescription.Frequency
            };
        }

        public List<Prescription> GetAll()
        {
            var modelPrescription = _prescriptionRepository.GetAll();
            if (modelPrescription == null)
                return new List<Prescription>();

            return modelPrescription.Select(m => new Prescription
            {
                Id = m.Id,
                HistoryId = m.HistoryId,
                MedicineId = m.MedicineId,
                Quantity = m.Quantity,
                Frequency = m.Frequency
            }).ToList();
        }

        public List<Prescription> GetByHistory(int historyId)
        {
            throw new NotImplementedException();
        }

        public (bool Ok, string Message) Update(Prescription prescription)
        {
            if (prescription == null)
                return (false, "Prescription cannot be null.");

            var existing = _prescriptionRepository.Get(prescription.Id);
            if (existing == null)
                return (false, "Prescription not found.");

            var modelPrescription = new WPF.Hospital.Model.Prescription
            {
                Id = prescription.Id,
                HistoryId = prescription.HistoryId,
                MedicineId = prescription.MedicineId,
                Quantity = prescription.Quantity,
                Frequency = prescription.Frequency
            };
            
            _prescriptionRepository.Update(modelPrescription);
            return (true, "Prescription updated successfully.");
       
        }
    }
}


       