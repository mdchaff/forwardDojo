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
    public class HomeController : Controller {
        private MyContext _context;
        public HomeController (MyContext context) {
            _context = context;
        }
        // [HttpGet]
        // [Route ("test")]
        // public IActionResult IndexC () {
        //     return RedirectToAction(SecondController IndexB);
        // }
        //*************************************************************************************** 
        //                                 LOGIN, LOGOUT, REGISTER
        //***************************************************************************************   
        [HttpGet]
        [Route ("")]
        public IActionResult Index () {
            System.Console.WriteLine ("\n\n\t===   LOG & REG   ===\n\n");
            List<User> Users = _context.Users.ToList ();
            ViewBag.allUsers = Users;
            foreach (var user in Users) {System.Console.WriteLine (user.FirstName);}
            if (HttpContext.Session.GetString ("LogErrors") != null) {
                ViewBag.LogErrors = HttpContext.Session.GetString ("LogErrors");
                return View ("A_LogReg");
            }
            else {return View ("A_LogReg");}
        }
        [HttpPost]
        [Route ("")]
        public IActionResult Register (User model) {
            var hasErrors = false;

            // First Name
            if ((model.FirstName == null)) { ViewBag.FirstNameErrors = " Required"; hasErrors = true; } else if (model.FirstName.All (Char.IsLetter) == false) { ViewBag.FirstNameErrors = " No Symbols"; hasErrors = true; }

            // Last Name
            if ((model.LastName == null)) { ViewBag.LastNameErrors = " Required"; hasErrors = true; } else if (model.LastName.All (Char.IsLetter) == false) { ViewBag.LastNameErrors = " No Symbols"; hasErrors = true; }

            // Email
            if ((model.Email == null)) { ViewBag.EmailErrors = " Required"; hasErrors = true; }
            User CheckEmail = _context.Users.SingleOrDefault (user => user.Email == model.Email);
            if (CheckEmail != null) { ViewBag.EmailErrors = " Email in use"; hasErrors = true; }

            // Password
            if ((model.PasswordC == null)) { ViewBag.PasswordCErrors = " Required"; hasErrors = true; }
            if ((model.Password == null)) { ViewBag.PasswordErrors = " Required"; hasErrors = true; } else {
                string thePassword = model.Password;
                bool PasswordLetter = thePassword.Any (Char.IsLetter);
                bool PasswordNumber = thePassword.Any (Char.IsDigit);
                bool PasswordSpecChar = thePassword.Any (Char.IsPunctuation);
                System.Console.WriteLine ("\t===  PASSWORD  FORMAT  ===");
                System.Console.WriteLine ("\t===   PASSWORD ERROR   ===");
                System.Console.WriteLine ("\tLetter: " + PasswordLetter);
                System.Console.WriteLine ("\t Number: " + PasswordNumber);
                System.Console.WriteLine ("\tSpecial: " + PasswordSpecChar);
                if ((PasswordLetter == false) || (PasswordNumber == false) || (PasswordSpecChar == false)) {
                    ViewBag.PasswordLongErrors = " Password requires at least one Letter & Number & Symbol.";
                    hasErrors = true;
                }
            }

            if ((ModelState.IsValid) && (hasErrors == false)) {
                _context.Add (model);
                PasswordHasher<User> Hasher = new PasswordHasher<User> ();
                model.Password = Hasher.HashPassword (model, model.Password);
                model.PasswordC = Hasher.HashPassword (model, model.PasswordC);
                _context.SaveChanges ();
                User CurrentUser = _context.Users.SingleOrDefault(user => user.Email == model.Email);
                HttpContext.Session.SetInt32 ("User_ID", CurrentUser.User_ID);
                return RedirectToAction ("Dashboard");
            } else { return View ("A_LogReg"); }
        }
        [HttpPost]
        [Route ("Login")]
        public IActionResult Login (string Email, string Password) {
            if (Email != null) {
                User CheckEmail = _context.Users.SingleOrDefault (user => user.Email == Email);
                if (CheckEmail != null) {
                    var Hasher = new PasswordHasher<User> ();
                    if (0 != Hasher.VerifyHashedPassword (CheckEmail, CheckEmail.Password, Password)) {
                        HttpContext.Session.SetInt32 ("User_ID", CheckEmail.User_ID);
                        System.Console.WriteLine ("\n\n\t===   REDIRECTING ===\n\n");
                        return RedirectToAction ("Dashboard");
                        // return RedirectToAction("TestSite");
                    } else {
                        HttpContext.Session.SetString ("LogErrors", "Incorrect Email or Password");
                        return RedirectToAction ("Index");
                    }
                } else {
                    HttpContext.Session.SetString ("LogErrors", "Incorrect Email or Password");
                    return RedirectToAction ("Index");
                }
            } else {
                ViewBag.LogErrors = "Incorrect Email or Password";
                HttpContext.Session.SetString ("LogErrors", "Incorrect Email or Password");
                return RedirectToAction ("Index"); // return View ("AB_MainLogReg");
            }
        }
        [HttpGet]
        [Route ("Logout")]
        public IActionResult Logout () {
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index");
        }
        //*************************************************************************************** 
        //                         PROFILE, DASHBOARD & DETAILS PAGE
        //***************************************************************************************
        [HttpGet]
        [Route ("MyPage")]//Change route to username
        public IActionResult MyPage () {
            if(HttpContext.Session.GetInt32("User_ID") == null) {return RedirectToAction("Index");}
            User CurrentUser = _context.Users.SingleOrDefault(user => user.User_ID == HttpContext.Session.GetInt32("User_ID"));
            
            List<Joined> UserJoineds = _context.Joineds.Where(Joined => Joined.User.Equals(CurrentUser)).ToList();
            List<Job> allUserJobs = new List<Job>();




            System.Console.WriteLine("\n\n\tXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            foreach(var job in UserJoineds){
                Job thisJob = _context.Jobs.SingleOrDefault(jobx => jobx.Job_ID == job.Job_ID);
                allUserJobs.Add(thisJob);
                // System.Console.WriteLine("\t"+thisJob.company);
            }
            System.Console.WriteLine("\tXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX\n\n");









            ViewBag.theUser = (CurrentUser.FirstName + " " + CurrentUser.LastName);
            ViewBag.allJobs = allUserJobs;

            return View ("D_myPage");
            
        }
        [HttpGet]
        [Route ("Dashboard")]
        public IActionResult Dashboard () {
            if(HttpContext.Session.GetInt32("User_ID") == null) {return RedirectToAction("Index");}
            User CurrentUser = _context.Users.SingleOrDefault (user => user.User_ID == HttpContext.Session.GetInt32 ("User_ID"));
            
            // string url = "https://remoteok.io/api?ref=producthunt";
            string filepath = "Controllers/tempJobs.json";
            string result = string.Empty;
            using (StreamReader r = new StreamReader (filepath)) {
                var json = r.ReadToEnd ();
                var jobj = JArray.Parse (json);
                ViewBag.allJobs = jobj;
            }
            if (HttpContext.Session.GetInt32 ("User_ID") == null) {return RedirectToAction ("Index");}
            ViewBag.theUser = CurrentUser.FirstName + " " + CurrentUser.LastName;

            return View ("B_Dashboard");
        }

        [HttpPost]
        [Route ("DetailsP")]
        public IActionResult DetailsP (Job model) {
            User CurrentUser = _context.Users.SingleOrDefault(user => user.User_ID == HttpContext.Session.GetInt32("User_ID"));
            HttpContext.Session.SetInt32("User_ID", CurrentUser.User_ID);
            // HttpContext.Session.SetString("slug", model.slug);
            HttpContext.Session.SetString("epoch", model.epoch);
            HttpContext.Session.SetString("date", model.date.ToString());
            HttpContext.Session.SetString("company", model.company);
            HttpContext.Session.SetString("position", model.position);
            HttpContext.Session.SetString("description", model.description);
            HttpContext.Session.SetString("url", model.url);

            return RedirectToAction("DetailsPage");}
        [HttpGet]
        [Route ("Details")]
        public IActionResult DetailsPage () {
            if(HttpContext.Session.GetInt32("User_ID") == null) {return RedirectToAction("Index");}
            User CurrentUser = _context.Users.SingleOrDefault(user => user.User_ID == HttpContext.Session.GetInt32("User_ID"));
            ViewBag.theUser = HttpContext.Session.GetString ("theUser");

            // ViewBag.slug = HttpContext.Session.GetString("slug");
            ViewBag.epoch = HttpContext.Session.GetString("epoch");
            ViewBag.date = HttpContext.Session.GetString("date");
            ViewBag.company = HttpContext.Session.GetString("company");
            ViewBag.position = HttpContext.Session.GetString("position");
            ViewBag.description = HttpContext.Session.GetString("description");
            ViewBag.url = HttpContext.Session.GetString("url");
            ViewBag.theUser = CurrentUser.FirstName + " " + CurrentUser.LastName;

            System.Console.WriteLine("\n\n\t======  VIEWBAG  ======");
            System.Console.WriteLine("\t"+HttpContext.Session.GetString("epoch"));
            System.Console.WriteLine("\t"+HttpContext.Session.GetString("date"));
            System.Console.WriteLine("\t"+HttpContext.Session.GetString("company"));
            System.Console.WriteLine("\t"+HttpContext.Session.GetString("position"));
            System.Console.WriteLine("\t"+HttpContext.Session.GetString("description"));
            System.Console.WriteLine("\t"+HttpContext.Session.GetString("url"));
            System.Console.WriteLine("\t=======================");

            return View ("c_details");
        }
        //*************************************************************************************** 
        //                         ADD, DELETE & APPLY FOR JOB
        //***************************************************************************************
        [HttpPost]
        [Route("addJob")]
        public IActionResult addJob(Job model) {
            System.Console.WriteLine("\n\n\t____________ ADD JOB _____________");
            if(HttpContext.Session.GetInt32("User_ID") == null) {return RedirectToAction("Index");}
            User CurrentUser = _context.Users.SingleOrDefault(user => user.User_ID == HttpContext.Session.GetInt32("User_ID"));

            Job CurrentJob = new Job{
                // slug = model.slug,                      // 1
                epoch = model.epoch,                    // 2
                date = Convert.ToDateTime(model.date),  // 3
                company = model.company,                // 4
                position = model.position,              // 5
                description = model.description,        // 6
                url = model.url,                        // 7
                applied = false
            };







            System.Console.WriteLine("\n\n\t_________________________________");
            System.Console.WriteLine("\t"+model.company);
            System.Console.WriteLine("\t_________________________________\n\n");







            HttpContext.Session.SetString ("theJob", model.company);
            HttpContext.Session.SetString ("theUser", (CurrentUser.FirstName + " " + CurrentUser.LastName));
            _context.Add(CurrentJob); _context.SaveChanges();
            Joined newJoined = new Joined{
                User_ID = CurrentUser.User_ID,
                Job_ID = CurrentJob.Job_ID,
                User = CurrentUser,
                Job = CurrentJob
            };
            _context.Add(newJoined); _context.SaveChanges();
            HttpContext.Session.SetInt32("Job_ID", CurrentJob.Job_ID);
            return RedirectToAction("MyPage");
        }
        [HttpGet]
        [Route("Delete/{Job_ID}")]
        public IActionResult Delete(int Job_ID) {
            if(HttpContext.Session.GetInt32("User_ID") == null) {return RedirectToAction("Index");}
            if(HttpContext.Session.GetInt32("User_ID") == null) {
                return RedirectToAction("Index");
            }
            Job CurrentJob = _context.Jobs
                .SingleOrDefault(activity => activity.Job_ID == Job_ID);

            _context.Jobs.Remove(CurrentJob);
            _context.SaveChanges();
            return RedirectToAction("MyPage");
        }
        [HttpGet]
        [Route("DashB")]
        public IActionResult DashB() {
            return View("BB_Dashboard");
        }

        //*************************************************************************************** 
        //                                  TEST SITE
        //***************************************************************************************

        [HttpGet]
        [Route("TestSite")]
        public IActionResult TestSite() {
            string filepath = "Controllers/tempJobs.json";
            string result = string.Empty;
            using (StreamReader r = new StreamReader (filepath)) {
                var json = r.ReadToEnd ();
                var jobj = JArray.Parse (json);
                ViewBag.allJobs = jobj;
            }
            return View("about");
        }
        [HttpGet]
        [Route("about")]
        public IActionResult about() {
            return View("about");
        }

    }
}