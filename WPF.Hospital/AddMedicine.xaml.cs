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
    /// Interaction logic for AddMedicine.xaml
    /// </summary>
    public partial class AddMedicine : Window
    {
        private readonly IMedicineService _medicineService;

        public AddMedicine(IMedicineService medicineService)
        {
            InitializeComponent();
            _medicineService = medicineService;
        }

        private void btnAddMedicine_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Name and Description must not be empty.");
                return;
            }

            // Create DTO entity
            var newMedicine = new DTO.Medicine
            {
                Name = name,
                Brand = description
            };

            // Persist to database via service
            var result = _medicineService.Add(newMedicine);
            if (!result.Ok)
            {
                MessageBox.Show(result.Message);
                return;
            }

            _medicineService.Save();

            MessageBox.Show("Medicine added successfully!");
            this.Close();
        }
    }
}

