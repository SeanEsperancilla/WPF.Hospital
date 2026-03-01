using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPatientService _patientService;
        private readonly IHistoryService _historyService;
        private readonly IDoctorService _doctorService;
        private readonly IPrescriptionService _prescriptionService;
        private readonly IMedicineService _medicineService;

        public MainWindow(IPatientService patientService, IHistoryService historyService, 
                    IDoctorService doctorService,IPrescriptionService prescriptionService,
                    IMedicineService medicineService)
        {
            InitializeComponent(); 
            _patientService = patientService;
            _historyService = historyService;
            _doctorService = doctorService;
            _prescriptionService = prescriptionService;
            _medicineService = medicineService;
            this.WindowState = WindowState.Maximized;
        }

        // ---------------- PATIENT ----------------
        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            AddPatient addPatient = new AddPatient(_patientService);
            addPatient.ShowDialog();
        }

        private void btnUpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            UpdatePatient updatePatient = new UpdatePatient(_patientService);
            updatePatient.ShowDialog();
        }

        private void btnDeletePatient_Click(object sender, RoutedEventArgs e)
        {
            DeletePatient deletePatient = new DeletePatient(_patientService, _historyService, _prescriptionService);
            deletePatient.ShowDialog();
        }


        private void btnAllPatients_Click(object sender, RoutedEventArgs e)
        {
            AllPatients allPatients = new AllPatients(_patientService);
            allPatients.ShowDialog();
        }

        // ---------------- MEDICAL HISTORY ----------------
        private void btnAddHistory_Click(object sender, RoutedEventArgs e)
        {
            AddHistory addHistory = new AddHistory(_historyService);
            addHistory.ShowDialog();
        }

        private void btnDeleteHistory_Click(object sender, RoutedEventArgs e)
        {
            DeleteHistory deleteHistory = new DeleteHistory(_historyService);
            deleteHistory.ShowDialog();
        }

        private void btnAllHistory_Click(object sender, RoutedEventArgs e)
        {
            AllHistory allHistory = new AllHistory(_historyService);
            allHistory.ShowDialog();
        }

        // ---------------- PRESCRIPTION ----------------
        private void btnAddPrescription_Click(object sender, RoutedEventArgs e)
        {
            AddPrescription addPrescription = new AddPrescription(_historyService, _medicineService, _prescriptionService);
            addPrescription.ShowDialog();
        }

        private void btnDeletePrescription_Click(object sender, RoutedEventArgs e)
        {
            DeletePrescription deletePrescription = new DeletePrescription(_prescriptionService);
            deletePrescription.ShowDialog();
        }

        private void btnAllPrescription_Click(object sender, RoutedEventArgs e)
        {
            AllPrescription allPrescription = new AllPrescription(_prescriptionService);
            allPrescription.ShowDialog();
        }

        // ---------------- MEDICINE ----------------
        private void btnAddMedicine_Click(object sender, RoutedEventArgs e)
        {
            AddMedicine addMedicine = new AddMedicine(_medicineService);
            addMedicine.ShowDialog();
        }

        private void btnUpdateMedicine_Click(object sender, RoutedEventArgs e)
        {
            UpdateMedicine updateMedicine = new UpdateMedicine(_medicineService);
            updateMedicine.ShowDialog();
        }

        private void btnDeleteMedicine_Click(object sender, RoutedEventArgs e)
        {
            DeleteMedicine deleteMedicine = new DeleteMedicine(_medicineService);
            deleteMedicine.ShowDialog();
        }

        

        // ---------------- DOCTOR ----------------
        private void btnAddDoctor_Click(object sender, RoutedEventArgs e)
        {
            AddDoctor addDoctor = new AddDoctor(_doctorService);
            addDoctor.ShowDialog();
        }

        private void btnUpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            UpdateDoctor updateDoctor = new UpdateDoctor(_doctorService);
            updateDoctor.ShowDialog();
        }

        private void btnDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            DeleteDoctor deleteDoctor = new DeleteDoctor(_doctorService );
            deleteDoctor.ShowDialog();
        }

        private void btnAllDoctor_Click(object sender, RoutedEventArgs e)
        {
            AllDoctor allDoctor = new AllDoctor(_doctorService);
            allDoctor.ShowDialog();
        }

        private void btnAllMedicine_Click(object sender, RoutedEventArgs e)
        {
            AllMedicine allMedicine = new AllMedicine(_medicineService);
            allMedicine.ShowDialog();
        }
    }
}
