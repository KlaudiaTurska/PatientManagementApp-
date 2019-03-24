using System;

namespace PatientManagementApp.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int Angle { get; set; }
        public DateTime Duration { get; set; }
        public Patient Patient { get; set; }
        public string PatientId { get; set; }
    }
}