using EVisaApplicationSystem.Models;
using EVisaApplicationSystem.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVisaApplicationSystem.Controllers
{

    /// <summary>
    /// Controller responsible for handling admin-related operations.
    /// </summary>
    public class AdminController : Controller
    {
        private readonly AdminRepository adminRepositoryObject;

        /// <summary>
        /// Initializes a new instance of the AdminController class.
        /// </summary>
        public AdminController()
        {
            adminRepositoryObject = new AdminRepository();
        }




        /// <summary>
        /// GET action method for the admin home page.
        /// </summary>
        /// <returns>The admin home view.</returns>
        public ActionResult AdminHome()
        {
            return View();
        }






        /// <summary>
        /// GET action method for the admin registration page.
        /// </summary>
        /// <returns>The admin registration view.</returns>
        public ActionResult AdminRegister()
        {

            return View();
        }



        /// <summary>
        /// POST action method for handling admin registration.
        /// </summary>
        /// <param name="adminObject">The admin object containing registration details.</param>
        /// <param name="imageFile">The uploaded image file.</param>
        /// <returns>An ActionResult representing the result of the registration operation.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminRegister(Admin adminObject, HttpPostedFileBase imageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(imageFile.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/AdminProfileImages/"), fileName);

                        // Save the image file to the server
                        imageFile.SaveAs(filePath);

                        // Save the image path in the database using the repository
                        adminRepositoryObject.InsertAdmin(adminObject, filePath);

                        return RedirectToAction("Login", "Login"); // Redirect to the login page 
                    }
                    else
                    {
                        // If no image is uploaded, return to the upload view with an error message
                        ModelState.AddModelError("", "Please select an image to upload.");
                        return View(adminObject);
                    }
                }

                return View(adminObject);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(adminObject);
            }
        }



    }
}