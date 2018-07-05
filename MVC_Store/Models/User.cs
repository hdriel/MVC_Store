using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Store.Models
{
    public class User
    {
        [Required(ErrorMessage = "You must enter a first name!")]
        public String FirstName { get; set; }

        public String LastName { get; set; }

        [Required(ErrorMessage = "You must enter a password")]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public String Password { get; set; }

        //[Required(ErrorMessage = "You must enter a password again")]
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match.")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string RepeatPassword { get; set; }

        [RegularExpression("^[0]{1}[5]{1}[0-9]{1}[-]{1}[0-9]{3}[-]{1}[0-9]{4}$", ErrorMessage = "Please provide number like this 05x-xxx-xxxx")]
        public String Phone { get; set; }

        public String Address { get; set; }

        public String PicName { get; set; }

        [Key]
        [Required(ErrorMessage = "You must enter a email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "You must enter your birthdate")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        
        //[RegularExpression("^[0-9]{3}$", ErrorMessage = "Please enter your age")]
        public int age { get; set; }

        public String sex { get; set; }
        
        public short isAdmin { get; set; }

        public void UpdateAge()
        {
            if (sex == null) sex = "Male";
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            age = today.Year - BirthDate.Year;
            // Do stuff with it.
            if (BirthDate > today.AddYears(-age))
                age--;
        }
    }
}