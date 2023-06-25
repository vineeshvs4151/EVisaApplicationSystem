// LoginController.cs
using System;
using System.Web.Mvc;
using EVisaApplicationSystem.Models;
using EVisaApplicationSystem.Repository;

namespace EVisaApplicationSystem.Controllers
{
    /// <summary>
    /// Controller responsible for handling user login and logout operations.
    /// </summary>
    public class LoginController : Controller
    {
        private readonly LoginRepository loginRepository;
        


        /// <summary>
        /// Initializes a new instance of the LoginController class.
        /// </summary>
        public LoginController()
        {
            loginRepository = new LoginRepository();
        }




        /// <summary>
        /// GET action method for the login page.
        /// </summary>
        /// <returns>The login view.</returns>
        public ActionResult Login()
        {

            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                return RedirectToAction("UserIndex", "User"); // User is logged in, redirect to user index page 
            }
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            {
                return RedirectToAction("AdminHome", "Admin"); // admin is logged in, redirect to admin index page 
            }
            return View();
        }

        /// <summary>
        /// POST action method for handling user login.
        /// </summary>
        /// <param name="loginModel">The login model containing user credentials.</param>
        /// <returns>An ActionResult representing the result of the login operation.</returns>
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (loginRepository.AuthenticateAdmin(loginModel, out string loginAdminId))
                    {
                        // Start the session and store the login admin ID
                        Session["LoginAdminId"] = loginAdminId;
                        Session["AdminUsername"] = loginModel.Username;
                        return RedirectToAction("AdminHome", "Admin");
                    }

                    if (loginRepository.AuthenticateUser(loginModel, out string loginUserId))
                    {
                        // Start the session and store the login user ID
                        Session["LoginUserId"] = loginUserId;
                        Session["Username"] = loginModel.Username;
                        return RedirectToAction("UserIndex", "User");
                    }     
                }
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }
            catch 
            {
                TempData["ErrorMessage"] = "Error occured during login";
                return View(loginModel);
            }

            
        }




        /// <summary>
        /// Action method for user logout.
        /// </summary>
        /// <returns>An ActionResult representing the result of the logout operation.</returns>
        public ActionResult Logout()
        {
            // Clear the session and redirect to the login page
            Session.Clear();
            return RedirectToAction("Login");
        }



    }

}
