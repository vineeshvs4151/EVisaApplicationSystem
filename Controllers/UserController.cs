using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using EVisaApplicationSystem.Models;
using EVisaApplicationSystem.Repository;

namespace EVisaApplicationSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository userRepository;

        public UserController()
        {
            userRepository = new UserRepository();
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <remarks>
        /// This method retrieves a list of states from the user repository and assigns it to the ViewBag.States property.
        /// </remarks>
        /// <returns>The action result for the register view.</returns>
        public ActionResult Register()
        {
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            {
                return RedirectToAction("AdminHome", "Admin"); // admin is logged in, redirect to admin index page 
            }
            // Retrieve the list of states from the user repository
            List<States> states = userRepository.GetStates();

            // Assign the list of states to the ViewBag.States property
            ViewBag.States = states;

            // Return the action result for the register view
            return View();
        }

     

        /// <summary>
        /// Retrieves cities based on the selected state.
        /// </summary>
        /// <param name="StateValue">The value of the selected state.</param>
        /// <returns>A JSON result containing the cities for the selected state.</returns>
        public JsonResult Getcity(string StateValue)
        {
            // Retrieve the list of cities based on the selected state
            List<Cities> cityslects = userRepository.GetCitiesByStates(StateValue).ToList();

            // Return the cities as a JSON result with JsonRequestBehavior set to AllowGet
            return Json(cityslects, JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// Registers a new user with the provided information and image file.
        /// </summary>
        /// <param name="user">The User object containing the user information.</param>
        /// <param name="imageFile">The uploaded image file.</param>
        /// <returns>The action result for the appropriate view.</returns>
        [HttpPost]
        public ActionResult Register(User user, HttpPostedFileBase imageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        // Extract the file name and create the file path
                        string fileName = Path.GetFileName(imageFile.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/UserProfileImages/"), fileName);

                        // Save the image file to the server
                        imageFile.SaveAs(filePath);

                        // Save the image path in the database using the repository
                        userRepository.InsertUser(user, filePath);

                        return RedirectToAction("Login", "Login"); // Redirect to login page
                    }
                }

                // If the image is not uploaded or the model state is not valid, return to the register view with error messages
                List<States> states = userRepository.GetStates();
                ViewBag.States = states;
                ModelState.AddModelError("", "Please select an image to upload.");
                return View();
            }
            catch (Exception ex)
            {
                // If an exception occurs, return to the register view with the error message
                List<States> states = userRepository.GetStates();
                ViewBag.States = states;
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }





        /// <summary>
        /// Displays the user index view.
        /// </summary>
        /// <returns>The action result for the user index view.</returns>
        public ActionResult UserIndex()
        {
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            {
                return RedirectToAction("AdminHome", "Admin"); // admin is logged in, redirect to admin index page 
            }
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                return View();
            }
            // Redirect to the login page if the user is not logged in
            return RedirectToAction("Login", "Login");
        }





        /// <summary>
        /// Retrieves user details by ID and returns the corresponding view.
        /// </summary>
        /// <returns>The view containing the user details.</returns>
        public ActionResult GetUserById()
        {
            try
            {
                string loginAdminId = Session["LoginAdminId"] as string;
                if (!string.IsNullOrEmpty(loginAdminId))
                {
                    return RedirectToAction("AdminHome", "Admin"); // admin is logged in, redirect to admin index page 
                }
                string loginUserId = Session["LoginUserId"] as string;
                if (!string.IsNullOrEmpty(loginUserId))
                {

                    int userId = Convert.ToInt32(Session["LoginUserId"]);

                    UserRepository userRepositoryObject = new UserRepository();
                    List<User> UserDetailList = userRepositoryObject.GetUserById(userId);

                    return View(UserDetailList);
                }
                return RedirectToAction("Login", "Login");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "" + ex.Message;
                return RedirectToAction("Login", "Login");
            }

        }






        /// <summary>
        /// Displays the edit profile page.
        /// </summary>
        /// <returns>The ActionResult representing the edit profile view.</returns>
        public ActionResult EditProfile()
        {
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            {
                return RedirectToAction("AdminHome", "Admin"); // admin is logged in, redirect to admin index page 
            }
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                int userId = Convert.ToInt32(loginUserId);

                UserRepository userRepositoryObject = new UserRepository();
                List<User> UserDetailList = userRepositoryObject.GetUserById(userId);

                User user = UserDetailList.FirstOrDefault();

                if (user != null)
                {
                    List<States> states = userRepository.GetStates(); // Retrieve the list of states for dropdown selection
                    ViewBag.States = states; // Pass the list of states to the view using ViewBag

                    return View(user); // Return the edit profile view with the user object and list of states
                }
                return RedirectToAction("Login", "Login");
            }
            // If the user is not logged in or no user data found, redirect to the login page
            return RedirectToAction("Login", "Login");
        }






        /// <summary>
        /// Updates the user profile with the provided user details and image file.
        /// </summary>
        /// <param name="user">The User object containing the updated user details.</param>
        /// <param name="imageFile">The image file posted by the user.</param>
        /// <returns>The ActionResult representing the appropriate view after the profile update.</returns>
        [HttpPost]
        public ActionResult UpdateUserProfile(User user, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                
                
                    string loginUserId = Session["LoginUserId"] as string;

                    if (!string.IsNullOrEmpty(loginUserId))
                    {
                        int userId = Convert.ToInt32(loginUserId);

                        if (imageFile != null && imageFile.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(imageFile.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/UserProfileImages/"), fileName);

                            // Save the image file to the server
                            imageFile.SaveAs(filePath);

                            // Save the image path in the database using the repository
                            userRepository.UpdateUserProfileByUser(user, filePath, userId);
                        }
                        else
                        {
                            // Retrieve existing image path from the database
                            string existingImagePath = userRepository.GetUserImagePath(userId);

                            // Update the user data without modifying the image path
                            userRepository.UpdateUserProfileByUser(user, existingImagePath, userId);
                        }

                        // Redirect to the user profile page after updating the profile
                        return RedirectToAction("GetUserById", "User");
                    }
                
            }

            // If the model state is not valid, return to the edit profile view with the user object
            return View("EditProfile", user);
        }






        /// <summary>
        /// Deletes the user profile of the currently logged-in user.
        /// </summary>
        /// <returns>The ActionResult representing the appropriate view after deleting the profile.</returns>
        public ActionResult DeleteUserProfile()
        {
            string loginUserId = Session["LoginUserId"] as string;

            if (!string.IsNullOrEmpty(loginUserId))
            {
                int userId = Convert.ToInt32(Session["LoginUserId"]);

                UserRepository userRepositoryObject = new UserRepository();

                // Delete the user profile using the repository
                userRepositoryObject.DeleteUserProfileByUser(userId);

                // Redirect to the home page after deleting the profile
                return RedirectToAction("Register", "User");
            }
            else
            {
                // If the user is not logged in, redirect to the login page
                return RedirectToAction("Login", "Login");
            }
        }


    }

}   

