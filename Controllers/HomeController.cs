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
    public class HomeController : Controller
    {
        private HomeRepository homeRepositoryObject;



        /// <summary>
        /// Initializes a new instance of the HomeController class.
        /// </summary>
        public HomeController()
        {
            homeRepositoryObject = new HomeRepository();
        }




        /// <summary>
        /// Renders the Index view.
        /// </summary>
        /// <returns>Returns an ActionResult representing the Index view.</returns>
        /// <remarks>
        /// This method is invoked when a GET request is made to the "Index" action.
        /// It simply returns the Index view, allowing users to view and interact with the home page.
        /// </remarks>
        public ActionResult Index()
        {
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                return RedirectToAction("UserIndex", "User"); // User is logged in, redirect to user index page 
            }
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            {
                return RedirectToAction("AdminHome", "Admin"); // Admin is logged in, redirect to admin index page 
            }
            return View(); // Common landing page for both users and admins
        }



        public ActionResult About()
        {
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                return RedirectToAction("UserIndex", "User"); // User is logged in, redirect to user index page 
            }
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            {
                return RedirectToAction("AdminHome", "Admin"); // User is logged in, redirect to admin index page 
            }
            return View();
        }





        /// <summary>
        /// Displays the view for users to add a message through a contact form.
        /// </summary>
        /// <returns>The view for adding a contact form message.</returns>
        public ActionResult AddContactFormMessageByUser()
        {
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                return RedirectToAction("UserIndex", "User"); // User is logged in, redirect to user index page 
            }
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            {
                return RedirectToAction("AdminHome", "Admin"); // Admin is logged in, redirect to admin index page 
            }
            return View();
        }






        /// <summary>
        /// Adds a contact form message submitted by a user and returns the corresponding ActionResult.
        /// </summary>
        /// <param name="contactObject">The Contact object representing the message submitted by the user.</param>
        /// <returns>The ActionResult indicating the result of adding the contact form message.</returns>
        [HttpPost]
        public ActionResult AddContactFormMessageByUser(Contact contactObject)
        {
            try
            {
                // Check if the model state is valid
                if (ModelState.IsValid)
                {
                    // Add the contact form message using the home repository
                    if (homeRepositoryObject.AddContactFormMessageByUser(contactObject))
                    {
                        // Return the view
                        return View();
                    }
                }

                // If the model state is not valid or the message addition failed, return the view

                // Return the view to display the form again, with validation errors if any
                return View();
            }
            catch (Exception ex)
            {
                // If an exception occurs, store the error message in TempData

                // Store the error message in TempData to display to the user
                TempData["ErrorMessage"] = ex.Message;

                // Return the view with the contactObject to allow the user to correct and resubmit the form
                return View(contactObject);
            }
        }





        /// <summary>
        /// Retrieves all contact form messages entered by users for the admin and displays them in a view.
        /// </summary>
        /// <returns>The view displaying all contact form messages entered by users.</returns>
        public ActionResult GetAllContactFormMessagesForAdmin()
        {
            string loginUserId = Session["LoginUserId"] as string;
            if (!string.IsNullOrEmpty(loginUserId))
            {
                return RedirectToAction("UserIndex", "User"); // User is logged in, redirect to user index page 
            }
            string loginAdminId = Session["LoginAdminId"] as string;
            if (!string.IsNullOrEmpty(loginAdminId))
            {
                List<Contact> messageDetailList = homeRepositoryObject.GetAllContactFormMessagesForAdmin();
                return View(messageDetailList);
            }
            return RedirectToAction("Login", "Login");
        }





        /// <summary>
        /// Retrieves contact form message details based on the specified message status.
        /// </summary>
        /// <param name="status">The status of the messages to retrieve.</param>
        /// <returns>An ActionResult representing the view containing the list of contact form message details.</returns>
        public ActionResult RetrievesContactFormMessageDetailsByStatus(string status)
        {
            try
            {
                string loginUserId = Session["LoginUserId"] as string;
                if (!string.IsNullOrEmpty(loginUserId))
                {
                    return RedirectToAction("UserIndex", "User"); // User is logged in, redirect to user index page 
                }
                string loginAdminId = Session["LoginAdminId"] as string;
                if (!string.IsNullOrEmpty(loginAdminId))
                {
                    // Retrieve the contact form message details based on the status using the home repository
                    List<Contact> messageDetailList = homeRepositoryObject.RetrievesContactFormMessageDetailsByStatus(status);
                    // Return the view with the retrieved message details
                    return View(messageDetailList);
                }
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                // If an exception occurs, store the error message in TempData

                // Store the error message in TempData to display to the user
                TempData["ErrorMessage"] = ex.Message;

                // Redirect to the GetAllContactFormMessagesForAdmin action
                return RedirectToAction("GetAllContactFormMessagesForAdmin", "Home");
            }
        }






        /// <summary>
        /// Retrieves contact form message details based on the specified message ID.
        /// </summary>
        /// <param name="messageId">The ID of the message to retrieve.</param>
        /// <returns>An ActionResult representing the view containing the contact form message details.</returns>
        public ActionResult RetrievesContactFormMessageDetailsById(int messageId)
        {
            try
            {
                string loginUserId = Session["LoginUserId"] as string;
                if (!string.IsNullOrEmpty(loginUserId))
                {
                    return RedirectToAction("UserIndex", "User"); // User is logged in, redirect to user index page 
                }
                string loginAdminId = Session["LoginAdminId"] as string;
                if (!string.IsNullOrEmpty(loginAdminId))
                {
                    List<Contact> messageDetailList = homeRepositoryObject.RetrievesContactFormMessageDetailsById(messageId);
                    Contact contactObject = messageDetailList.FirstOrDefault();
                    return View(contactObject);
                }
                return RedirectToAction("Login", "Login");
               
            }
            catch (Exception ex)
            {
                // If an exception occurs, store the error message in TempData

                // Store the error message in TempData to display to the user
                TempData["ErrorMessage"] = ex.Message;

                // Redirect to the GetAllContactFormMessagesForAdmin action
                return RedirectToAction("GetAllContactFormMessagesForAdmin", "Home");
            }
        }





        /// <summary>
        /// Deletes the contact form message details with the specified message ID by an admin.
        /// </summary>
        /// <param name="messageId">The ID of the contact form message to delete.</param>
        /// <returns>An ActionResult representing the result of the deletion operation.</returns>
        public ActionResult DeleteContactFormMessageDetailsByAdmin(int messageId)
        {
            try
            { 
                // Call the DeleteContactFormMessageDetailsByAdmin method in the home repository to delete the message
                homeRepositoryObject.DeleteContactFormMessageDetailsByAdmin(messageId);

                // Set success message in TempData
                TempData["SuccessMessage"] = "Message deleted successfully.";

                // Redirect to the GetAllContactFormMessagesForAdmin action
                return RedirectToAction("GetAllContactFormMessagesForAdmin", "Home");
            }
            catch (Exception ex)
            {
                // If an exception occurs, store the error message in TempData

                // Store the error message in TempData to display to the user
                TempData["ErrorMessage"] = ex.Message;

                // Redirect to the GetAllContactFormMessagesForAdmin action
                return RedirectToAction("GetAllContactFormMessagesForAdmin", "Home");
            }
        }






        /// <summary>
        /// Adds an admin reply to the contact form message with the specified message ID.
        /// </summary>
        /// <param name="contactObject">The Contact object containing the admin reply message.</param>
        /// <param name="messageId">The ID of the contact form message.</param>
        /// <returns>An ActionResult representing the result of adding the admin reply.</returns>
        [HttpPost]
        public ActionResult AddAdminReplyToContactFormMessage(Contact contactObject, int messageId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string loginUserId = Session["LoginUserId"] as string;
                    if (!string.IsNullOrEmpty(loginUserId))
                    {
                        return RedirectToAction("UserIndex", "User"); // User is logged in, redirect to user index page 
                    }
                    string loginAdminId = Session["LoginAdminId"] as string;
                    if (!string.IsNullOrEmpty(loginAdminId))
                    {
                        // Call the AddAdminReplyToContactFormMessage method in the home repository to add the admin reply
                        if (homeRepositoryObject.AddAdminReplyToContactFormMessage(contactObject, messageId))
                        {
                            // Set success message in TempData
                            TempData["SuccessMessage"] = "Message replied successfully.";

                            // Redirect to the GetAllContactFormMessagesForAdmin action
                            return RedirectToAction("GetAllContactFormMessagesForAdmin", "Home");
                        }
                    }
                    return RedirectToAction("Login", "Login");
                    
                }

                // Return the view if the model state is not valid
                return View();
            }
            catch (Exception ex)
            {
                // If an exception occurs, store the error message in TempData

                // Store the error message in TempData to display to the user
                TempData["ErrorMessage"] = ex.Message;

                // Redirect to the GetAllContactFormMessagesForAdmin action
                return RedirectToAction("GetAllContactFormMessagesForAdmin", "Home");
            }
        }

    }
}
