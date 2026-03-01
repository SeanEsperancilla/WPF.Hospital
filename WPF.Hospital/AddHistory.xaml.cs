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
using WPF.Hospital.Service;
using WPF.Hospital.ViewModel;

namespace WPF.Hospital
{
    /// <summary>
    /// Interaction logic for AddHistory.xaml
    /// </summary>
    public partial class AddHistory : Window
    {
        private readonly IHistoryService _historyService;
        private readonly List<PatientViewModel> _patients;
        private readonly List<DoctorViewModel> _doctors;

        public AddHistory(IHistoryService historyService)
        {
            InitializeComponent();
            _historyService = historyService;

            // Load patients into ComboBox
            cbPatients.ItemsSource = _historyService.GetAllPatients()
                .Select(p => new PatientViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    Birthdate = p.Birthdate
                }).ToList();

            // Load doctors into ComboBox
            cbDoctors.ItemsSource = _historyService.GetAllDoctors()
                .Select(d => new DoctorViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName
                }).ToList();
        }

        private void btnAddHistory_Click(object sender, RoutedEventArgs e)
        {
            if (cbPatients.SelectedItem is PatientViewModel patient &&
                cbDoctors.SelectedItem is DoctorViewModel doctor)
            {
                string procedure = txtProcedure.Text.Trim();
                if (string.IsNullOrWhiteSpace(procedure))
                {
                    MessageBox.Show("Procedure must not be empty.");
                    return;
                }

                // Map ViewModels → DTO with nested Patient and Doctor
                var history = new DTO.History
                {
                    Patient = new DTO.Patient
                    {
                        Id = patient.Id,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        Age = patient.Age,
                        Birthdate = patient.Birthdate
                    },
                    Doctor = new DTO.Doctor
                    {
                        Id = doctor.Id,
                        FirstName = doctor.FirstName,
                        LastName = doctor.LastName
                    },
                    Procedure = procedure
                };

                var result = _historyService.Add(history);
                if (!result.Ok)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                _historyService.Save();
                MessageBox.Show("Medical history added successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select both a patient and a doctor.");
            }
        }
    }
}