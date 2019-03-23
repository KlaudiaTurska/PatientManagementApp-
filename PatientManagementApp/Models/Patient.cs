using System.Collections.Generic;

namespace PatientManagementApp.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public string Description { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<Exercise> Exercises { get; set; }
    }
}