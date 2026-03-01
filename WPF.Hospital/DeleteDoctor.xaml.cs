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
    /// Interaction logic for DeleteDoctor.xaml
    /// </summary>
    public partial class DeleteDoctor : Window
    {
        private readonly IDoctorService _doctorService;

        public DeleteDoctor(IDoctorService doctorService)
        {
            InitializeComponent();
            _doctorService = doctorService;
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            // Map Doctor DTOs to DoctorViewModel for the DataGrid
            dgDoctors.ItemsSource = _doctorService.GetAll()
                .Select(d => new DoctorViewModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName
                })
                .ToList();
        }

        private void btnDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (dgDoctors.SelectedItem is DoctorViewModel doctor)
            {
                var result = MessageBox.Show(
                    $"Delete doctor {doctor.FirstName} {doctor.LastName}?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var deleteResult = _doctorService.Delete(doctor.Id);
                    MessageBox.Show(deleteResult.Message);

                    if (deleteResult.Ok)
                    {
                        LoadDoctors(); // Refresh DataGrid
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a doctor to delete.");
            }
        }
    }
}
