using PatientManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientManagementApp.Repositories
{
    public class ExerciseRepository
    {
        private readonly ApplicationDbContext context;

        public ExerciseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Complete()
        {
            context.SaveChanges();
        }

        //Dodawanie ćwiczeń
        public void AddExercise(Exercise exercise)
        {
            context.Exercises.Add(exercise);
        }

        //Usuwanie ćwiczenia
        public void DeleteExercise(Exercise exercise)
        {
            context.Exercises.Remove(exercise);
        }

        //Usuwanie ćwiczeń
        public void DeleteExercises(IEnumerable<Exercise> exercises)
        {
            context.Exercises.RemoveRange(exercises);
        }

        //Wyszukiwanie ćwiczenia na podstawie id
        public Exercise GetExerciseById(int id)
        {
            return context.Exercises.Where(p => p.Id == id).FirstOrDefault();
        }

        //Zwrócenie wszystkich ćwiczeń dla danego pacjenta
        public IEnumerable<Exercise> GetAllPatientExercises(int id)
        {
            return context.Exercises.Where(u => u.Patient.Id == id).ToList();
        }
    }
}