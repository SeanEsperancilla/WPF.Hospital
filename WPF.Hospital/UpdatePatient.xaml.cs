using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UpdatePatient.xaml
    /// </summary>
    public partial class UpdatePatient : Window
    {
        private readonly IPatientService _patientService;

        public UpdatePatient(IPatientService patientService)
        {
            InitializeComponent();
            _patientService = patientService;
            dgPatients.ItemsSource = new ObservableCollection<PatientViewModel>(
                _patientService.GetAll().Select(p => new PatientViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    Birthdate = p.Birthdate
                })
            );
        }

        private void btnUpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem is PatientViewModel patient)
            {
                // Validation
                if (string.IsNullOrWhiteSpace(patient.FirstName) || string.IsNullOrWhiteSpace(patient.LastName))
                {
                    MessageBox.Show("First and Last Name must not be empty.");
                    return;
                }
                if (patient.Age <= 0)
                {
                    MessageBox.Show("Age must be greater than 0.");
                    return;
                }
                if (patient.Birthdate >= DateTime.Today)
                {
                    MessageBox.Show("Birthdate must be earlier than today.");
                    return;
                }

                dgPatients.SelectedItem = patient;
                var selected = dgPatients.SelectedItem as PatientViewModel;
                if (selected != null)
                {
                    // Map to entity if needed
                    var entity = new DTO.Patient
                    {
                        Id = selected.Id,
                        FirstName = selected.FirstName,
                        LastName = selected.LastName,
                        Age = selected.Age,
                        Birthdate = selected.Birthdate
                    };

                    // Update via service
                    _patientService.Update(entity);
                    _patientService.Save();
                    MessageBox.Show("Patient updated successfully!");
                    dgPatients.ItemsSource = _patientService.GetAll();

                }
                else
                {
                    MessageBox.Show("Select a patient to update.");
                }
            }
        }
    }
}
