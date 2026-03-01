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
    /// Interaction logic for DeleteHistory.xaml
    /// </summary>
    public partial class DeleteHistory : Window
    {
        private readonly IHistoryService _historyService;

        public DeleteHistory(IHistoryService historyService)
        {
            InitializeComponent();
            _historyService = historyService;
            LoadHistories();
        }

        private void LoadHistories()
        {
            dgHistories.ItemsSource = _historyService.GetAll()
                .Select(h => new HistoryViewModel
                {
                    Id = h.Id,
                    PatientName = $"{h.Patient.FirstName} {h.Patient.LastName}",
                    DoctorName = $"{h.Doctor.FirstName} {h.Doctor.LastName}",
                    Procedure = h.Procedure
                })
                .ToList();
        }

        private void btnDeleteHistory_Click(object sender, RoutedEventArgs e)
        {
            if (dgHistories.SelectedItem is HistoryViewModel history)
            {
                var result = MessageBox.Show(
                    $"Delete history record for {history.PatientName} ({history.Procedure})?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var deleteResult = _historyService.Delete(history.Id);
                    MessageBox.Show(deleteResult.Message);

                    if (deleteResult.Ok)
                    {
                        LoadHistories(); // Refresh DataGrid
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a history record to delete.");
            }
        }
    }
}

