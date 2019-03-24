using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientManagementApp.ViewModels
{
    //Klasa widoku modelu do udostępnienia listy ćwiczeń wraz z id pacjenta
    public class ExerciseContainerViewModel
    {
        public int PatientId { get; set; }

        public List<ExerciseViewModel> Exercises { get; set; }
    }
}