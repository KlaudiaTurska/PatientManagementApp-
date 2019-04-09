using PatientManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PatientManagementApp.Repositories
{
    public class PatientRepository
    {
        private readonly ApplicationDbContext context;

        public PatientRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Complete()
        {
            context.SaveChanges();
        }

        //Dodawanie pacjentów
        public void AddPatient(Patient patient)
        {
            context.Patients.Add(patient);
        }

        //Usuwanie pacjentów
        public void DeletePatient(Patient patient)
        {
            context.Patients.Remove(patient);
        }

        //Wybór jednego pacjenta na podstawie numeru id
        public Patient GetPatientById(int id)
        {
            return context.Patients.Where(u => u.Id == id).FirstOrDefault();
        }

        //Wybór jednego pacjenta na podstawie numeru pesel
        public Patient GetPatientByPesel(string pesel)
        {
            return context.Patients.Where(u => u.Pesel == pesel).FirstOrDefault();
        }

        //Zwrócenie listy pacjentów rehabilitantowi
        public IEnumerable<Patient> GetAllPhysiotherapistPatients(string physiotherapistId)
        {
            return context.Patients.Where(u => u.UserId == physiotherapistId).ToList();
        }
    }
}