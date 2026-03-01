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
    /// Interaction logic for UpdateDoctor.xaml
    /// </summary>
    public partial class UpdateDoctor : Window
    {
        private readonly IDoctorService _doctorService;

        public UpdateDoctor(IDoctorService doctorService)
        {
            InitializeComponent();
            _doctorService = doctorService;

            // Bind to ObservableCollection for editing
            dgDoctors.ItemsSource = new ObservableCollection<DoctorViewModel>(
                _doctorService.GetAll().Select(d => new DoctorViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName
                })
            );
        }

        private void btnUpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (dgDoctors.SelectedItem is DoctorViewModel doctor)
            {
                // Validation
                if (string.IsNullOrWhiteSpace(doctor.FirstName) || string.IsNullOrWhiteSpace(doctor.LastName))
                {
                    MessageBox.Show("First and Last Name must not be empty.");
                    return;
                }

                // Map to DTO
                var entity = new DTO.Doctor
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName
                };

                // Update via service
                var result = _doctorService.Update(entity);
                if (!result.Ok)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                _doctorService.Save();

                MessageBox.Show("Doctor updated successfully!");
                dgDoctors.ItemsSource = _doctorService.GetAll(); // refresh
            }
            else
            {
                MessageBox.Show("Select a doctor to update.");
            }
        }
    }

}
