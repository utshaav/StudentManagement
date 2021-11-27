using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage ="Please Enter your full name!!")]
        [Display(Name = "Full Name")]
        public string StudentName { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage ="Please enter your email address!!")]
        [EmailAddress]
        public string Email { get; set; }   
    }
}