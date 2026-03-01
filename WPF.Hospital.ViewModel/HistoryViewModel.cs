using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Hospital.ViewModel
{
    public class HistoryViewModel
    {
        public int Id { get; set; }

        // Keep numeric IDs if needed for operations
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        // Add display properties for the DataGrid
        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        public string Procedure { get; set; }
    }

}
