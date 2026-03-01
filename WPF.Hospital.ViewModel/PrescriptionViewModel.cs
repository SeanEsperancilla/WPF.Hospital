using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Hospital.ViewModel
{
    public class PrescriptionViewModel
    {
        public int Id { get; set; }
        public int HistoryId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public string Frequency { get; set; } = string.Empty;

    }

}
