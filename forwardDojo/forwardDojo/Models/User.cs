using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace forwardDojo.Models {
    public class User {
//====================================================
        [Key]
        public int User_ID { get; set; }

//===============      FIRST NAME      ===============

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

//===============      LAST NAME      ================

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

//===============         EMAIL        ===============

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

//===============       PASSWORD      ===============

        [Required(ErrorMessage = "*Required")]
        [MinLength(8)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

//===============       PASSWORDC     ===============

        [Compare("Password", ErrorMessage=" Passwords do not match")]
        [Display(Name = "Confirm Password:")]
        public string PasswordC { get; set; }

//===========  USERS JOBS SAVED/INTRESTED  ===========
        public List<Joined> Joineds { get; set; }
        public User () {
                Joineds = new List<Joined> ();
        }
//====================================================
        string FileName { get; set; }
    }
}