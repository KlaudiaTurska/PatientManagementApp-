using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementApp.ViewModels
{
    public class PatientViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pesel jest wymagany")]
        [StringLength(11, ErrorMessage = "Wprowadź poprawny pesel", MinimumLength = 11)]
        public string Pesel { get; set; }

        [MaxLength(500)]
        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}