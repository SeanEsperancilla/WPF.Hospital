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
    /// Interaction logic for AddPrescription.xaml
    /// </summary>
    public partial class AddPrescription : Window
    {
        private readonly IHistoryService _historyService;
        private readonly IMedicineService _medicineService;
        private readonly IPrescriptionService _prescriptionService;

        public AddPrescription(IHistoryService historyService,
                               IMedicineService medicineService,
                               IPrescriptionService prescriptionService)
        {
            InitializeComponent();
            _historyService = historyService;
            _medicineService = medicineService;
            _prescriptionService = prescriptionService;

            // Load histories and medicines into ComboBoxes
            cmbHistory.ItemsSource = _historyService.GetAll();
            cmbMedicine.ItemsSource = _medicineService.GetAll();

            DataContext = new PrescriptionViewModel();
        }

        private void btnAddPrescription_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PrescriptionViewModel)DataContext;

            // Validation
            if (vm.HistoryId <= 0)
            {
                MessageBox.Show("A history record must be selected.");
                return;
            }
            if (vm.MedicineId <= 0)
            {
                MessageBox.Show("A medicine must be selected.");
                return;
            }
            if (vm.Quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.");
                return;
            }
            if (string.IsNullOrWhiteSpace(vm.Frequency))
            {
                MessageBox.Show("Frequency must not be empty.");
                return;
            }

            // Save prescription
            _prescriptionService.Add(new DTO.Prescription()
            {
                History = new DTO.History { Id = vm.HistoryId },
                Medicine = new DTO.Medicine { Id = vm.MedicineId },
                Quantity = vm.Quantity,
                Frequency = vm.Frequency
            });

            MessageBox.Show("Prescription added successfully!");

            // Clear inputs
            vm.Quantity = 0;
            vm.Frequency = string.Empty;
            txtQuantity.Text = string.Empty;
            txtFrequency.Text = string.Empty;
        }
    }

}
