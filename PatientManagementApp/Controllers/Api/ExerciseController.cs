using PatientManagementApp.Models;
using PatientManagementApp.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace PatientManagementApp.Controllers.Api
{
    public class ExerciseController : ApiController
    {
        private ExerciseRepository exerciseRepository;
        private PatientRepository patientRepository;
        private GraphRepository graphRepository;
        private ApplicationDbContext context;

        public ExerciseController()
        {
            context = new ApplicationDbContext();
            exerciseRepository = new ExerciseRepository(context);
            patientRepository = new PatientRepository(context);
            graphRepository = new GraphRepository(context);
        }

        [HttpPost]
        public IHttpActionResult AddDataToExercise(AndroidRequest request)
        {
            if(request == null)
            {
                return NotFound();
            }

            var patient = patientRepository.GetPatientByPesel(request.Pesel);
            var exercise = exerciseRepository.GetExerciseById(request.ExerciseId);

            if(patient == null || exercise == null)
            {
                return NotFound();
            }

            List<GraphData> test = new List<GraphData>();
            request.Angle.ForEach(p =>
                    test.Add(new GraphData()
                    {
                        PatinetId = patient.Id,
                        ExerciseId = exercise.Id,
                        yValue = p,
                        xValue = request.Date,
                        CorrectMeasure = p < 30 && p > 10 ? true : false,
                    }));

            graphRepository.AddGraphData(test);
            context.SaveChanges();

            return Ok();
        }
    }
}
