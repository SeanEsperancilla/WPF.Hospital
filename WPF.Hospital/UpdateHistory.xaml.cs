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
    /// Interaction logic for UpdateHistory.xaml
    /// </summary>
    public partial class UpdateHistory : Window
    {
        private readonly IHistoryService _historyService;

        public UpdateHistory(IHistoryService historyService)
        {
            InitializeComponent();
            _historyService = historyService;
            dgHistories.ItemsSource = _historyService.GetAll();
        }

        private void btnUpdateHistory_Click(object sender, RoutedEventArgs e)
        {
            if (dgHistories.SelectedItem is HistoryViewModel history)
            {
                if (history.PatientId <= 0 || history.DoctorId <= 0)
                {
                    MessageBox.Show("Patient ID and Doctor ID must be valid.");
                    return;
                }

                //_historyService.Update(history);
                var updatedhistory = new HistoryViewModel
                {
                    Id = history.Id,
                    PatientId = history.PatientId,
                    DoctorId = history.DoctorId,
                    Procedure = history.Procedure
                };
                MessageBox.Show("History updated successfully!");
                dgHistories.ItemsSource = _historyService.GetAll();
            }
            else
            {
                MessageBox.Show("Select a history record to update.");
            }
        }
    }

}
