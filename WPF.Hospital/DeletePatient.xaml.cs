using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.Hospital.Repository;
using WPF.Hospital.Service;
using WPF.Hospital.ViewModel;
using WPF.Hospital.Model;

namespace WPF.Hospital
{
    /// <summary>
    /// Interaction logic for DeletePatient.xaml
    /// </summary>
    public partial class DeletePatient : Window
    {
        private readonly IPatientService _patientService;
        private readonly IHistoryService _historyService;
        private readonly IPrescriptionService _prescriptionService;

        public DeletePatient(IPatientService patientService, IHistoryService historyService,
                     IPrescriptionService prescriptionService)
        {
            InitializeComponent();
            _patientService = patientService;
            _historyService = historyService;
            _prescriptionService = prescriptionService;

            LoadPatients();
        }



        private void LoadPatients()
        {
            // Map Patient DTOs to PatientViewModel for the DataGrid
            dgPatients.ItemsSource = _patientService.GetAll()
                .Select(p => new PatientViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    Birthdate = p.Birthdate
                })
                .ToList();
        }

        private void btnDeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem is PatientViewModel patient)
            {
                var result = MessageBox.Show(
                    $"Delete patient {patient.FirstName} {patient.LastName}?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _patientService.Delete(patient.Id);
                        _patientService.Save();

                        MessageBox.Show("Patient deleted successfully!");

                        // Refresh DataGrid
                        LoadPatients();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, "Delete Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                _patientService.Delete(patient.Id);
                _patientService.Save();

            }
            else
            {
                MessageBox.Show("Select a patient to delete.");
            }
        }
    }

}

