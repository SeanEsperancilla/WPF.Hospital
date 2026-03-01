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
    /// Interaction logic for UpdateMedicine.xaml
    /// </summary>
    public partial class UpdateMedicine : Window
    {
        private readonly IMedicineService _medicineService;

        public UpdateMedicine(IMedicineService medicineService)
        {
            InitializeComponent();
            _medicineService = medicineService;

            // Bind to ObservableCollection for editing
            dgMedicines.ItemsSource = new ObservableCollection<MedicineViewModel>(
                _medicineService.GetAll().Select(m => new MedicineViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Brand = m.Brand
                })
            );
        }

        private void btnUpdateMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedicines.SelectedItem is MedicineViewModel medicine)
            {
                // Validation
                if (string.IsNullOrWhiteSpace(medicine.Name))
                {
                    MessageBox.Show("Medicine name must not be empty.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(medicine.Brand))
                {
                    MessageBox.Show("Brand must not be empty.");
                    return;
                }

                // Map to DTO
                var entity = new DTO.Medicine
                {
                    Id = medicine.Id,
                    Name = medicine.Name,
                    Brand = medicine.Brand
                };

                // Update via service
                var result = _medicineService.Update(entity);
                if (!result.Ok)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                _medicineService.Save();

                MessageBox.Show("Medicine updated successfully!");
                dgMedicines.ItemsSource = _medicineService.GetAll(); // refresh
            }
            else
            {
                MessageBox.Show("Select a medicine to update.");
            }
        }
    }
}
