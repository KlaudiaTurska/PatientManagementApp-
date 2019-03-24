using Microsoft.AspNet.Identity;
using PatientManagementApp.Models;
using PatientManagementApp.Repositories;
using PatientManagementApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private PatientRepository patientRepository;
        private ExerciseRepository exerciseRepository;
        private ApplicationDbContext context;

        public HomeController()
        {
            context = new ApplicationDbContext();
            patientRepository = new PatientRepository(context);
            exerciseRepository = new ExerciseRepository(context);
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
            //Sprawdzenie czy model jest zgodny z założeniami
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
        
        public ActionResult PatientList()
        {
            var userId = User.Identity.GetUserId();
            //Wyciągnięcie listy pacjentów dla zalogowanego rehablitanta
            IEnumerable<Patient> patients = patientRepository.GetAllPhysiotherapistPatients(userId);
            return View(patients);
        }

        public ActionResult PatientExersicesList()
        {
            //Wyciąganięcie listy ćwiczeń dla danego pacjenta
            return View();
        }
    }
}