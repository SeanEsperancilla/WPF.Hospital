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
    /// Interaction logic for UpdatePrescription.xaml
    /// </summary>
    public partial class UpdatePrescription : Window
    {
        private readonly IPrescriptionService _prescriptionService;

        public UpdatePrescription(IPrescriptionService prescriptionService)
        {
            InitializeComponent();
            _prescriptionService = prescriptionService;
            dgPrescriptions.ItemsSource = _prescriptionService.GetAll();
        }

        private void btnUpdatePrescription_Click(object sender, RoutedEventArgs e)
        {
            if (dgPrescriptions.SelectedItem is PrescriptionViewModel prescription)
            {
                if (prescription.Quantity <= 0)
                {
                    MessageBox.Show("Quantity must be greater than 0.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(prescription.Frequency))
                {
                    MessageBox.Show("Frequency must not be empty.");
                    return;
                }

                //_prescriptionService.Update(prescription);
                var Prescription = new PrescriptionViewModel
                {
                    Id = prescription.Id,
                    HistoryId = prescription.HistoryId,
                    MedicineId = prescription.MedicineId,
                    Quantity = prescription.Quantity,
                    Frequency = prescription.Frequency
                };
                MessageBox.Show("Prescription updated successfully!");
                dgPrescriptions.ItemsSource = _prescriptionService.GetAll();
            }
            else
            {
                MessageBox.Show("Select a prescription to update.");
            }
        }
    }
}
