using Highsoft.Web.Mvc.Charts;
using Microsoft.AspNet.Identity;
using PatientManagementApp.Models;
using PatientManagementApp.Repositories;
using PatientManagementApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PatientManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private PatientRepository patientRepository;
        private ExerciseRepository exerciseRepository;
        private GraphRepository graphRepository;
        private ApplicationDbContext context;

        public HomeController()
        {
            context = new ApplicationDbContext();
            patientRepository = new PatientRepository(context);
            exerciseRepository = new ExerciseRepository(context);
            graphRepository = new GraphRepository(context);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPatient(PatientViewModel patientViewModel)
        {
            //Czy model jest zgodny z założeniami?
            if (ModelState.IsValid)
            {
                var patient = patientRepository.GetPatientByPesel(patientViewModel.Pesel);
                //Czy jest już pacjent o takim numerze pesel?
                if (patient == null)
                {
                    patientRepository.AddPatient(new Patient()
                    {  
                        Name = patientViewModel.Name,
                        LastName = patientViewModel.LastName,
                        Description = patientViewModel.Description,
                        Pesel = patientViewModel.Pesel,
                        UserId = User.Identity.GetUserId(),
                    });

                    patientRepository.Complete();

                    return RedirectToAction("PatientList");
                }
                ModelState.AddModelError(string.Empty ,"Istnieje już pacjent o takim numerze pesel");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditPatient(int id)
        {
            var patient = patientRepository.GetPatientById(id);

            var patientViewModel = new PatientViewModel()
            {
                Id = id,
                Name = patient.Name,
                LastName = patient.LastName,
                Description = patient.Description,
                Pesel = patient.Pesel
            };
                
            return View(patientViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPatient(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                var patient = patientRepository.GetPatientById(patientViewModel.Id);

                patient.Name = patientViewModel.Name;
                patient.LastName = patientViewModel.LastName;
                patient.Description = patientViewModel.Description;
                patient.Pesel = patientViewModel.Pesel;

                patientRepository.Complete();

                return RedirectToAction("PatientList");
            }
            
            return View(patientViewModel);
        }
        
        public ActionResult DeletePatient(int id)
        {
            var patient = patientRepository.GetPatientById(id);

            var exercises = exerciseRepository.GetAllPatientExercises(patient.Id);
            //Usuwanie wszystkich cwiczeń dla pacjenta
            exerciseRepository.DeleteExercises(exercises);
            exerciseRepository.Complete();

            patientRepository.DeletePatient(patient);
            patientRepository.Complete();
            return RedirectToAction("PatientList");
        }
        
        public ActionResult PatientList()
        {
            var userId = User.Identity.GetUserId();
            //Wyciągnięcie listy pacjentów dla zalogowanego rehablitanta
            IEnumerable<Patient> patients = patientRepository.GetAllPhysiotherapistPatients(userId);
            return View(patients);
        }

        public ActionResult PatientExersicesList(int patientId)
        {
            var userId = User.Identity.GetUserId();
            //Wyciąganięcie listy ćwiczeń dla danego pacjenta
            var exercises = exerciseRepository.GetAllPatientExercises(patientId);

            //Utworzenie listy ćwiczeń
            ExerciseContainerViewModel exerciseContainer = new ExerciseContainerViewModel()
            {
                PatientId = patientId,
                Exercises = new List<ExerciseViewModel>()
            };

            //Wypełnienie listy ćwiczeniami
            foreach(var exercise in exercises)
            {
                exerciseContainer.Exercises.Add(new ExerciseViewModel()
                {
                    Id = exercise.Id,
                    Angle = exercise.Angle,
                    Description = exercise.Description,
                    Duration = exercise.Duration,
                    NumberOfRepetitions = exercise.NumberOfRepetitions,
                    PatientId = patientId,
                    AdditionalInformations = exercise.AdditionalInformation
                });
            }

            return View(exerciseContainer);
        }

        public ActionResult Statistics(int patientId)
        {
            StatisticsViewModel model = new StatisticsViewModel();

            var xSerie = graphRepository.GetXSerie(patientId).ToList();
            var yData = graphRepository.GetData(patientId).ToList();

            List<double?> correctMeasures = new List<double?>();
            List<double?> incorrectMeasures = new List<double?>();

            for (int i =0; i < xSerie.Count(); i ++)
            {
                correctMeasures.Add(0);
                incorrectMeasures.Add(0);

                foreach(var y in yData)
                {
                    if(y.xValue == xSerie[i])
                    {
                        if (y.CorrectMeasure)
                        {
                            correctMeasures[i] += 1;
                        }
                        else
                        {
                            incorrectMeasures[i] += 1;
                        }
                    }
                }
            }
            correctMeasures.ForEach(p => model.correctSerie.Add(new ColumnSeriesData { Y = p }));
            incorrectMeasures.ForEach(p => model.incorrectSerie.Add(new ColumnSeriesData { Y = p }));
            xSerie.ForEach(p => model.xSerie.Add(p.ToShortDateString()));

            return View(model);
        }


        [HttpGet]
        public ActionResult AddExercise(int patientId)
        {
            var exercise = new ExerciseViewModel()
            {
                PatientId = patientId,
            };
            return View(exercise);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddExercise(ExerciseViewModel exerciseViewModel)
        {
            if (ModelState.IsValid)
            {
                var patient = patientRepository.GetPatientById(exerciseViewModel.PatientId);

                exerciseRepository.AddExercise(new Exercise()
                {
                    Angle = exerciseViewModel.Angle,
                    Description = exerciseViewModel.Description,
                    Duration = exerciseViewModel.Duration,
                    NumberOfRepetitions = exerciseViewModel.NumberOfRepetitions,
                    PatientId = exerciseViewModel.PatientId,
                    Patient = patient,
                    AdditionalInformation = exerciseViewModel.AdditionalInformations
                });

                exerciseRepository.Complete();

                return RedirectToAction("PatientExersicesList", new { patientId = exerciseViewModel.PatientId });
            }

            return View(exerciseViewModel);
        }

        [HttpGet]
        public ActionResult EditExercise(int id)
        {
            var exercise = exerciseRepository.GetExerciseById(id);
            var exerciseViewModel = new ExerciseViewModel()
            {
                Id = exercise.Id,
                Description = exercise.Description,
                Angle = exercise.Angle,
                Duration = exercise.Duration,
                PatientId = exercise.PatientId,
                NumberOfRepetitions = exercise.NumberOfRepetitions,
                AdditionalInformations = exercise.AdditionalInformation
            };

            return View(exerciseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditExercise(ExerciseViewModel exerciseViewModel)
        {
            if (ModelState.IsValid)
            {
                var exercise = exerciseRepository.GetExerciseById(exerciseViewModel.Id);

                exercise.NumberOfRepetitions = exerciseViewModel.NumberOfRepetitions;
                exercise.Angle = exerciseViewModel.Angle;
                exercise.Duration = exerciseViewModel.Duration;
                exercise.Description = exerciseViewModel.Description;
                exercise.AdditionalInformation = exerciseViewModel.AdditionalInformations;

                exerciseRepository.Complete();

                return RedirectToAction("PatientExersicesList", new { patientId = exerciseViewModel.PatientId});
            }

            return View(exerciseViewModel);
        }

        public ActionResult DeleteExercise(int id, int patientId)
        {
            var exercise = exerciseRepository.GetExerciseById(id);

            exerciseRepository.DeleteExercise(exercise);
            exerciseRepository.Complete();

            return RedirectToAction("PatientExersicesList", new { patientId });
        }
    }
}