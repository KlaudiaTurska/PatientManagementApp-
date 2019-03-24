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

        public void AddExercise(Exercise exercise)
        {
            context.Exercises.Add(exercise);
        }

        public void DeleteExercise(Exercise exercise)
        {
            context.Exercises.Remove(exercise);
        }

        public IEnumerable<Exercise> GetAllPatientExercises(string pesel)
        {
            return context.Exercises
                    .Where(u => u.Patient.Pesel == pesel)
                    .ToList();
        }
    }
}