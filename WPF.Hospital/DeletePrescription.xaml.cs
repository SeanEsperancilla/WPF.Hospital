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
    /// Interaction logic for DeletePrescription.xaml
    /// </summary>
    public partial class DeletePrescription : Window
    {
        private readonly IPrescriptionService _prescriptionService;

        public DeletePrescription(IPrescriptionService prescriptionService)
        {
            InitializeComponent();
            _prescriptionService = prescriptionService;
            dgPrescriptions.ItemsSource = _prescriptionService.GetAll();
        }

        private void btnDeletePrescription_Click(object sender, RoutedEventArgs e)
        {
            if (dgPrescriptions.SelectedItem is PrescriptionViewModel prescription)
            {
                var result = MessageBox.Show($"Delete prescription ID {prescription.Id}?", "Confirm Delete",
                                             MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _prescriptionService.Delete(prescription.Id);
                    MessageBox.Show("Prescription deleted successfully!");
                    dgPrescriptions.ItemsSource = _prescriptionService.GetAll();
                }
            }
            else
            {
                MessageBox.Show("Select a prescription to delete.");
            }
        }
    }

}
