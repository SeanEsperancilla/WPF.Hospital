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
    /// Interaction logic for DeleteMedicine.xaml
    /// </summary>
    public partial class DeleteMedicine : Window
    {
        private readonly IMedicineService _medicineService;

        public DeleteMedicine(IMedicineService medicineService)
        {
            InitializeComponent();
            _medicineService = medicineService;
            LoadMedicines();
        }

        private void LoadMedicines()
        {
            // Map Medicine DTOs to MedicineViewModel for the DataGrid
            dgMedicines.ItemsSource = _medicineService.GetAll()
                .Select(m => new MedicineViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Brand = m.Brand
                })
                .ToList();
        }

        private void btnDeleteMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedicines.SelectedItem is MedicineViewModel medicine)
            {
                var result = MessageBox.Show(
                    $"Delete medicine {medicine.Name}?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var deleteResult = _medicineService.Delete(medicine.Id);
                    MessageBox.Show(deleteResult.Message);

                    if (deleteResult.Ok)
                    {
                        // Refresh DataGrid
                        LoadMedicines();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a medicine to delete.");
            }
        }
    }
}

