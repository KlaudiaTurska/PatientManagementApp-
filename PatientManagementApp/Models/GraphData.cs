using System;

namespace PatientManagementApp.Models
{
    public class GraphData
    {
        public int Id { get; set; }
        public int PatinetId { get; set; }
        public int ExerciseId { get; set; }
        public DateTime xValue { get; set; }
        public decimal yValue { get; set; }
        public bool CorrectMeasure { get; set; }
    }
}