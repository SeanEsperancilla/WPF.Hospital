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
using WPF.Hospital.DTO;
using WPF.Hospital.Service;
using WPF.Hospital.ViewModel;

namespace WPF.Hospital
{
    /// <summary>
    /// Interaction logic for AllPatients.xaml
    /// </summary>
    public partial class AllPatients : Window
    {
        private readonly IPatientService _patientService;

        public AllPatients(IPatientService patientService)
        {
            InitializeComponent();
            _patientService = patientService;
            DataContext = new
            {
                Patients = _patientService.GetAll()
                .Select(p => new PatientViewModel()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    Birthdate = p.Birthdate
                })
            };
        }
    }
}

