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

namespace WPF.Hospital
{
    /// <summary>
    /// Interaction logic for AllDoctor.xaml
    /// </summary>
    public partial class AllDoctor : Window
    {
        private readonly IDoctorService _doctorService;

        public AllDoctor(IDoctorService doctorService)
        {
            InitializeComponent();
            _doctorService = doctorService;

            dgDoctors.ItemsSource = _doctorService.GetAll();
        }
    }
}
