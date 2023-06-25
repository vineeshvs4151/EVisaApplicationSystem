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
    public class ApplicationController : Controller
    {
        private readonly ApplicationRepository applicationRepository;

        public ApplicationController()
        {
            applicationRepository = new ApplicationRepository();
        }






        /// <summary>
        /// Action method for displaying the e-Visa application form for the user.
        /// </summary>
        /// <returns>Returns the ActionResult representing the view for the e-Visa application form.</returns>
        public ActionResult EVisaApplicationFormForUser()
        {
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                // Retrieve all countries and visa types from the repository
                List<Country> countries = applicationRepository.RetrievesAllCountriesForAdmin();
                List<Visatype> visaTypes = applicationRepository.RetrievesAllVisaTypeForAdmin();

                // Pass the countries and visa types to the view using ViewBag
                ViewBag.Countries = countries;
                ViewBag.VisaTypes = visaTypes;

                // Return the view for the e-Visa application form
                return View();
            }
            // Redirect to the login page if the user is not logged in
            return RedirectToAction("Login", "Login");
        }






        /// <summary>
        /// Handles the HTTP POST request for submitting an e-Visa application form for a user.
        /// </summary>
        /// <param name="application">The application model containing the form data.</param>
        /// <param name="certificateFile">The uploaded certificate file.</param>
        /// <returns>Returns the appropriate ActionResult based on the result of the operation.</returns>
        [HttpPost]
        public ActionResult EVisaApplicationFormForUser(Application application, HttpPostedFileBase certificateFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string loginUserId = Session["LoginUserId"] as string;
                    if (!string.IsNullOrEmpty(loginUserId))
                    {
                        int userId = Convert.ToInt32(loginUserId);
                        if (certificateFile != null && certificateFile.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(certificateFile.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/UserCertificatesImages/"), fileName);

                            // Save the uploaded image file to the server
                            certificateFile.SaveAs(filePath);

                            int visaTypeMonth = Convert.ToInt32(applicationRepository.GetVisaDuration(application.VisaType));
                            // Save the image path in the database using the repository
                            bool result = applicationRepository.EVisaApplicationFormForUser(application, filePath, userId,visaTypeMonth);
                            if (result)
                            {
                                // Redirect to the "GetApplicationByUserId" action if the application form is saved successfully
                                return RedirectToAction("RetrivesApplicationByUserIdForUser");
                            }
                            else
                            {
                                // Throw an exception if the application form failed to save
                                throw new Exception("Failed to save application form.");
                            }
                        }
                        else
                        {
                            // If no image is uploaded, add a model error and return to the view with an error message
                            ModelState.AddModelError("", "Please select an image to upload.");
                            return View(application);
                        }
                    }
                    else
                    {
                        // Redirect to the login page if the user is not logged in
                        return RedirectToAction("Login", "Login");
                    }
                }
                else
                {
                    // Retrieve all countries and visa types from the repository
                    List<Country> countries = applicationRepository.RetrievesAllCountriesForAdmin();
                    List<Visatype> visaTypes = applicationRepository.RetrievesAllVisaTypeForAdmin();

                    // Pass the countries and visa types to the view using ViewBag
                    ViewBag.Countries = countries;
                    ViewBag.VisaTypes = visaTypes;
                    // Return the view with the application model if the model state is invalid
                    return View(application);
                }
            }
            catch (Exception ex)
            {
                // Retrieve all countries and visa types from the repository
                List<Country> countries = applicationRepository.RetrievesAllCountriesForAdmin();
                List<Visatype> visaTypes = applicationRepository.RetrievesAllVisaTypeForAdmin();

                // Pass the countries and visa types to the view using ViewBag
                ViewBag.Countries = countries;
                ViewBag.VisaTypes = visaTypes;
                // Set an error message in TempData and return the view with the application model
                TempData["ErrorMessage"] = ex.Message;
                return View(application);
            }
        }





        /// <summary>
        /// Retrieves all e-Visa applications for the admin and returns them as a view.
        /// </summary>
        /// <returns>Returns the ActionResult representing the view containing the list of e-Visa applications.</returns>
        public ActionResult RetrievesAllApplicationForAdmin()
        {
            try
            {
                string loginAdminId = Session["LoginAdminId"] as string;
                if (!string.IsNullOrEmpty(loginAdminId))
                {
                    ApplicationRepository applicationRepositoryObject = new ApplicationRepository();
                    List<Application> applicationDetailList = applicationRepositoryObject.RetrievesAllApplicationForAdmin();

                    // Return the view with the list of e-Visa applications
                    return View(applicationDetailList);
                }
                string loginUserId = Session["LoginUserId"] as string;
                if (!string.IsNullOrEmpty(loginUserId))
                {
                    return RedirectToAction("UserIndex", "User");
                }
                // Redirect to the login page if the user is not logged in
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                // If an exception occurs, display the exception message as an error message
                TempData["ErrorMessage"] = ex.Message;

                // Return to the view with an empty model
                return View();
            }
        }







        /// <summary>
        /// Retrieves e-Visa applications with a specific status for administrators and displays them in a view.
        /// </summary>
        /// <param name="status">The status value to filter the applications.</param>
        /// <returns>Returns an ActionResult representing the view with a list of Application objects filtered by the specified status.</returns>
        public ActionResult RetrivesApplicationBystatusForAdmin(string status)
        {
            try
            {
                string loginAdminId = Session["LoginAdminId"] as string;
                if (!string.IsNullOrEmpty(loginAdminId))
                {
                    ApplicationRepository applicationRepositoryObject = new ApplicationRepository();

                    // Retrieve the list of applications with the specified status from the repository
                    List<Application> applicationDetailList = applicationRepositoryObject.RetrivesApplicationBystatusForAdmin(status);

                    // Return the view with the list of filtered applications
                    return View(applicationDetailList);
                }
                string loginUserId = Session["LoginUserId"] as string;
                if (!string.IsNullOrEmpty(loginUserId))
                {
                    return RedirectToAction("UserIndex", "User");
                }
                // Redirect to the login page if the user is not logged in
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                // If an exception occurs, display the exception message as an error message
                TempData["ErrorMessage"] = ex.Message;

                // Return to the view without passing any model
                return View();
            }
        }







        /// <summary>
        /// Retrieves e-Visa applications for a specific user and displays them in a view.
        /// </summary>
        /// <returns>Returns an ActionResult representing the view with a list of Application objects for the current user.</returns>
        public ActionResult RetrivesApplicationByUserIdForUser()
        {
            try
            {
                string loginUserId = Session["LoginUserId"] as string;
                if (!string.IsNullOrEmpty(loginUserId))
                {
                    // Retrieve the user ID from the session
                    int userId = Convert.ToInt32(Session["LoginUserId"]);

                    ApplicationRepository applicationRepositoryObject = new ApplicationRepository();

                    // Retrieve the list of applications for the current user from the repository
                    List<Application> applicationDetailList = applicationRepositoryObject.RetrivesApplicationByUserIdForUser(userId);

                    // Return the view with the list of applications for the user
                    return View(applicationDetailList);
                }
                // Redirect to the login page if the user is not logged in
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                // If an exception occurs, display the exception message as an error message
                TempData["ErrorMessage"] = ex.Message;

                // Return to the view without passing any model
                return RedirectToAction("EVisaApplicationFormForUser","Application");
            }
        }



        /// <summary>
        /// Retrieves application details by application ID for the admin user.
        /// </summary>
        /// <param name="applicationId">The ID of the application to retrieve.</param>
        /// <returns>
        ///     If the admin user is logged in, returns a view displaying the application details.
        ///     If the user is a regular user, redirects to the user index page.
        ///     If the user is not logged in, redirects to the login page.
        /// </returns>
        public ActionResult RetrivesApplicationByApplicationIdForAdmin(int applicationId)
        {
            // Check if the admin user is logged in
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            {
                // Retrieve the application details by the application ID
                List<Application> applicationDetailList = applicationRepository.RetrivesApplicationByApplicationId(applicationId);
                return View(applicationDetailList);
            }

            // Check if the regular user is logged in
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                // Redirect to the user index page
                return RedirectToAction("UserIndex", "User");
            }

            // Redirect to the login page if the user is not logged in
            return RedirectToAction("Login", "Login");
        }






        /// <summary>
        /// Retrieves application details by application ID for the regular user.
        /// </summary>
        /// <param name="applicationId">The ID of the application to retrieve.</param>
        /// <returns>
        ///     If the regular user is logged in, returns a view displaying the application details.
        ///     If the user is not logged in, redirects to the login page.
        /// </returns>
        public ActionResult RetrivesApplicationByApplicationIdForUser(int applicationId)
        {
            // Check if the regular user is logged in
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                // Retrieve the application details by the application ID
                List<Application> applicationDetailList = applicationRepository.RetrivesApplicationByApplicationId(applicationId);
                return View(applicationDetailList);
            }

            // Redirect to the login page if the user is not logged in
            return RedirectToAction("Login", "Login");
        }





        /// <summary>
        /// Updates the status of an application by an administrator.
        /// </summary>
        /// <param name="applicationId">The ID of the application to update.</param>
        /// <param name="status">The new status value for the application.</param>
        /// <returns>Returns an ActionResult representing the action to be taken after updating the application status.</returns>
        public ActionResult UpdateApplicationStatusByAdmin(int applicationId, string status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string loginAdminId = Session["LoginAdminId"] as string;
                    if (!string.IsNullOrEmpty(loginAdminId))
                    {
                        // Call the method in the application repository to update the application status
                        applicationRepository.UpdateApplicationStatusByAdmin(applicationId, status);

                        // Set a success message to be displayed
                        TempData["SuccessMessage"] = "Application status was updated successfully.";

                        // Redirect to the action that retrieves all applications for the administrator
                        return RedirectToAction("RetrievesAllApplicationForAdmin");
                    }
                    string loginUserId = Session["LoginUserId"] as string;
                    if (!string.IsNullOrEmpty(loginUserId))
                    {
                        return RedirectToAction("UserIndex", "User");
                    }
                    // Redirect to the login page if the user is not logged in
                    return RedirectToAction("Login", "Login");
                }
                TempData["ErrorMessage"] = "Failed to update application status.";
                return View();
            }
            catch (Exception ex)
            {
                // If an exception occurs, display the exception message as an error message
                TempData["ErrorMessage"] = ex.Message;

                // Return to the view without passing any model
                return View();
            }
        }




        /// <summary>
        /// Deletes an application by its ID for a specific user.
        /// </summary>
        /// <param name="applicationId">The ID of the application to delete.</param>
        /// <returns>The action result after deleting the application.</returns>
        public ActionResult DeleteApplicationByUser(int applicationId)
        {
            try
            {
                string loginUserId = Session["LoginUserId"] as string;
                if (!string.IsNullOrEmpty(loginUserId))
                {
                    applicationRepository.DeleteApplicationByUser(applicationId);
                    // Use TempData to store the success message and display it
                    TempData["SuccessMessage"] = "Your application request was deleted successfully.";
                    return RedirectToAction("RetrivesApplicationByUserIdForUser", "Application");
                }
                // Redirect to the login page if the user is not logged in
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                // If an exception occurs, display the exception message as an error message
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("RetrivesApplicationByUserIdForUser", "Application");
            }
        }






        /// <summary>
        /// Displays the view for adding a country by the administrator.
        /// </summary>
        /// <returns>The ActionResult representing the view for adding a country.</returns>
        public ActionResult AddCountryByAdmin()
        {
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                return RedirectToAction("UserIndex", "User");
            }
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            {
                return View();
            }
            // Redirect to the login page if the user is not logged in
            return RedirectToAction("Login", "Login");
        }





        /// <summary>
        /// Adds a country by the administrator.
        /// </summary>
        /// <param name="country">The Country object representing the country to be added.</param>
        /// <returns>The ActionResult representing the view for retrieving all countries for the administrator.</returns>
        [HttpPost]
        public ActionResult AddCountryByAdmin(Country country)
        {
            try
            {
                applicationRepository.AddCountryByAdmin(country);
                // Country added successfully
                return RedirectToAction("RetrievesAllCountriesForAdmin");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message); // Add error message to model state
                return View(country);
            }
        }







        /// <summary>
        /// Retrieves all countries for the administrator.
        /// </summary>
        /// <returns>The ActionResult representing the view for displaying all countries for the administrator.</returns>
        public ActionResult RetrievesAllCountriesForAdmin()
        {
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                return RedirectToAction("UserIndex", "User");
            }
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            { 
                List<Country> countryList = applicationRepository.RetrievesAllCountriesForAdmin();
                return View(countryList);
            }
            // Redirect to the login page if the user is not logged in
            return RedirectToAction("Login", "Login");
        }



        /// <summary>
        /// Deletes a country by the administrator.
        /// </summary>
        /// <param name="countryValue">The value of the country to be deleted.</param>
        /// <returns>The ActionResult representing the redirection to the view for displaying all countries for the administrator.</returns>
        public ActionResult DeleteCountryByAdmin(string countryValue)
        {
            try
            {
                applicationRepository.DeleteCountryByAdmin(countryValue);
                TempData["SuccessMessage"] = "Country was deleted successfully.";
                return RedirectToAction("RetrievesAllCountriesForAdmin", "Application");

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("RetrievesAllCountriesForAdmin", "Application");
            }
        }




        /// <summary>
        /// Renders the view for adding a new visa type by an administrator.
        /// </summary>
        /// <returns>The action result containing the view.</returns>
        public ActionResult AddVisaTypeByAdmin()
        {
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                return RedirectToAction("UserIndex", "User");
            }
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            {
                return View();
            }
            // Redirect to the login page if the user is not logged in
            return RedirectToAction("Login", "Login");

        }





        /// <summary>
        /// Adds a new visa type by an administrator.
        /// </summary>
        /// <param name="visaType">The visa type object containing the details of the new visa type.</param>
        /// <returns>The action result after adding the visa type.</returns>
        [HttpPost]
        public ActionResult AddVisaTypeByAdmin(Visatype visaType)
        {
            try
            {
                applicationRepository.AddVisaTypeByAdmin(visaType);
                return RedirectToAction("RetrievesAllVisaTypeForAdmin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message); // Add error message to model state
                return View(visaType);
            }
        }






        /// <summary>
        /// Retrieves all visa types for the administrator.
        /// </summary>
        /// <returns>The action result containing the view with the list of visa types.</returns>
        public ActionResult RetrievesAllVisaTypeForAdmin()
        {
            try
            {
                string loginUserId = Session["LoginUserId"] as string;
                if (!string.IsNullOrEmpty(loginUserId))
                {
                    return RedirectToAction("UserIndex", "User");
                }
                string loginAdminId = Session["LoginAdminId"] as string;
                if (!string.IsNullOrEmpty(loginAdminId))
                {
                    List<Visatype> visatypeList = applicationRepository.RetrievesAllVisaTypeForAdmin();
                    return View(visatypeList);
                }
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message); // Add error message to model state
                return View();
            }
        }





        /// <summary>
        /// Deletes a visa type by the administrator.
        /// </summary>
        /// <param name="visatypeValue">The value of the visa type to be deleted.</param>
        /// <returns>The action result redirecting to the view with the list of visa types.</returns>
        public ActionResult DeleteVisaTypeByAdmin(string visatypeValue)
        {
            try
            {
                applicationRepository.DeleteVisaTypeByAdmin(visatypeValue);
                TempData["SuccessMessage"] = "Visa type was deleted successfully.";
                return RedirectToAction("RetrievesAllVisaTypeForAdmin", "Application");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("RetrievesAllVisaTypeForAdmin", "Application");
            }
        }



    }
}