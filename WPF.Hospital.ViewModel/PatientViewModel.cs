using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Hospital.ViewModel
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime Birthdate { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
