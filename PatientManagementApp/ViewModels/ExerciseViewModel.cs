using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PatientManagementApp.ViewModels
{
    public class ExerciseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PatientId { get; set; }

        [MaxLength(500)]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Liczba powtórzeń")]
        [Range(1, 150, ErrorMessage = "Liczba powtórzeń powinna być między 1 a 50")]
        public int NumberOfRepetitions { get; set; }

        [Display(Name = "Kąt (10-30°)")]
        [Range(10,30, ErrorMessage = "Wartość kąta powinna być pomiędzy 10 a 30 stopni")]
        public int Angle { get; set; }

        [Display(Name = "Okres wykonywania ćwiczenia")]
        
        [DataType(DataType.Time)]
        public DateTime Duration { get; set; }

        [Display(Name = "Dodatkowe informacje")]
        public string AdditionalInformations { get; set; }
    }
}