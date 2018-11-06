using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using forwardDojo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
// --------------------------------
using System.Globalization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
// --------------------------------
using forwardDojo.Controllers;
// using StreamReader

namespace forwardDojo.Controllers {
    public class SecondController : Controller {
        private MyContext _context;
        public SecondController (MyContext context) {
            _context = context;
        }
        //*************************************************************************************** 
        //                                 LOGIN, LOGOUT, REGISTER
        //***************************************************************************************   
        [HttpGet]
        [Route ("Second")]
        public IActionResult IndexB () {
            return View ("test");
        }
        // [HttpPost]
        // [Route ("")]
        // public IActionResult Register (User model) {
        //     var hasErrors = false;

        //     // First Name
        //     if ((model.FirstName == null)) { ViewBag.FirstNameErrors = " Required"; hasErrors = true; } else if (model.FirstName.All (Char.IsLetter) == false) { ViewBag.FirstNameErrors = " No Symbols"; hasErrors = true; }

        //     // Last Name
        //     if ((model.LastName == null)) { ViewBag.LastNameErrors = " Required"; hasErrors = true; } else if (model.LastName.All (Char.IsLetter) == false) { ViewBag.LastNameErrors = " No Symbols"; hasErrors = true; }

        //     // Email
        //     if ((model.Email == null)) { ViewBag.EmailErrors = " Required"; hasErrors = true; }
        //     User CheckEmail = _context.Users.SingleOrDefault (user => user.Email == model.Email);
        //     if (CheckEmail != null) { ViewBag.EmailErrors = " Email in use"; hasErrors = true; }

        //     // Password
        //     if ((model.PasswordC == null)) { ViewBag.PasswordCErrors = " Required"; hasErrors = true; }
        //     if ((model.Password == null)) { ViewBag.PasswordErrors = " Required"; hasErrors = true; } else {
        //         string thePassword = model.Password;
        //         bool PasswordLetter = thePassword.Any (Char.IsLetter);
        //         bool PasswordNumber = thePassword.Any (Char.IsDigit);
        //         bool PasswordSpecChar = thePassword.Any (Char.IsPunctuation);
        //         System.Console.WriteLine ("\t===  PASSWORD  FORMAT  ===");
        //         System.Console.WriteLine ("\t===   PASSWORD ERROR   ===");
        //         System.Console.WriteLine ("\tLetter: " + PasswordLetter);
        //         System.Console.WriteLine ("\t Number: " + PasswordNumber);
        //         System.Console.WriteLine ("\tSpecial: " + PasswordSpecChar);
        //         if ((PasswordLetter == false) || (PasswordNumber == false) || (PasswordSpecChar == false)) {
        //             ViewBag.PasswordLongErrors = " Password requires at least one Letter & Number & Symbol.";
        //             hasErrors = true;
        //         }
        //     }

        //     if ((ModelState.IsValid) && (hasErrors == false)) {
        //         _context.Add (model);
        //         PasswordHasher<User> Hasher = new PasswordHasher<User> ();
        //         model.Password = Hasher.HashPassword (model, model.Password);
        //         model.PasswordC = Hasher.HashPassword (model, model.PasswordC);
        //         _context.SaveChanges ();
        //         User CurrentUser = _context.Users.SingleOrDefault(user => user.Email == model.Email);
        //         HttpContext.Session.SetInt32 ("User_ID", CurrentUser.User_ID);
        //         return RedirectToAction ("Dashboard");
        //     } else { return View ("A_LogReg"); }
        // }
        // [HttpPost]
        // [Route ("Login")]
        // public IActionResult Login (string Email, string Password) {
        //     if (Email != null) {
        //         User CheckEmail = _context.Users.SingleOrDefault (user => user.Email == Email);
        //         if (CheckEmail != null) {
        //             var Hasher = new PasswordHasher<User> ();
        //             if (0 != Hasher.VerifyHashedPassword (CheckEmail, CheckEmail.Password, Password)) {
        //                 HttpContext.Session.SetInt32 ("User_ID", CheckEmail.User_ID);
        //                 System.Console.WriteLine ("\n\n\t===   REDIRECTING ===\n\n");
        //                 return RedirectToAction ("Dashboard");
        //                 // return RedirectToAction("TestSite");
        //             } else {
        //                 HttpContext.Session.SetString ("LogErrors", "Incorrect Email or Password");
        //                 return RedirectToAction ("Index");
        //             }
        //         } else {
        //             HttpContext.Session.SetString ("LogErrors", "Incorrect Email or Password");
        //             return RedirectToAction ("Index");
        //         }
        //     } else {
        //         ViewBag.LogErrors = "Incorrect Email or Password";
        //         HttpContext.Session.SetString ("LogErrors", "Incorrect Email or Password");
        //         return RedirectToAction ("Index"); // return View ("AB_MainLogReg");
        //     }
        // }
        // [HttpGet]
        // [Route ("Logout")]
        // public IActionResult Logout () {
        //     HttpContext.Session.Clear ();
        //     return RedirectToAction ("Index");
        // }
    }
}