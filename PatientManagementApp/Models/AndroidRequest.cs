using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientManagementApp.Models
{
    public class AndroidRequest
    {
        public string Name { get; set; }
        public string Pesel { get; set; }
        public List<decimal> Angle { get; set; }
        public DateTime Date { get; set; }
        public int ExerciseId { get; set; }
    }
}