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
    /// Interaction logic for AddDoctor.xaml
    /// </summary>
    public partial class AddDoctor : Window
    {
        private readonly IDoctorService _doctorService;

        public AddDoctor(IDoctorService doctorService)
        {
            InitializeComponent();
            _doctorService = doctorService;
        }

        private void btnAddDoctor_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("First and Last Name must not be empty.");
                return;
            }

            // Create entity or ViewModel depending on what your service expects
            var newDoctor = new DTO.Doctor
            {
                FirstName = firstName,
                LastName = lastName
            };

            // Persist to database via service
            _doctorService.Add(newDoctor);
            _doctorService.Save(); 

            MessageBox.Show("Doctor added successfully!");
            this.Close();
        }
    }
}
