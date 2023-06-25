using EVisaApplicationSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EVisaApplicationSystem.Repository
{
    public class ApplicationRepository
    {


        private SqlConnection sqlConnectionObject;

        //To Handle connection related activities
        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["EvisaApplicationDBConnectionString"].ConnectionString;
            sqlConnectionObject = new SqlConnection(constr);
        }








        /// <summary>
        /// Saves the e-Visa application form for a user in the database.
        /// </summary>
        /// <param name="application">The application model containing the form data.</param>
        /// <param name="medicalCertificate">The path to the medical certificate file.</param>
        /// <param name="userId">The ID of the user submitting the application.</param>
        /// <returns>Returns true if the application form is successfully saved; otherwise, throws an exception.</returns>
        public bool EVisaApplicationFormForUser(Application application, string medicalCertificate, int userId,int visaTypeMonth)
        {
            Connection(); // Establish the database connection
            SqlCommand commandObject = new SqlCommand("SP_Application", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            sqlConnectionObject.Open();

            commandObject.CommandType = CommandType.StoredProcedure;
            // Add other parameters to the command
            commandObject.Parameters.AddWithValue("@Action", "InsertSP");
            commandObject.Parameters.AddWithValue("@Userid", userId);
            commandObject.Parameters.AddWithValue("@Visatype", application.VisaType);
            commandObject.Parameters.AddWithValue("@Passportnumber", application.PassportNumber);
            commandObject.Parameters.AddWithValue("@Country", application.Country);
            commandObject.Parameters.AddWithValue("@Entrydate", application.EntryDate);
            commandObject.Parameters.AddWithValue("@MedicalCertificate", medicalCertificate);
            DateTime entryDate = DateTime.Parse(application.EntryDate);
            DateTime duration = entryDate.AddMonths(visaTypeMonth);
            commandObject.Parameters.AddWithValue("@VisaDuration", duration);
            int result = commandObject.ExecuteNonQuery();
            sqlConnectionObject.Close();

            if (result == 0)
            {
                throw new Exception("Failed to create about entry");
            }
            else
            {
                return true;
            }
        }








        /// <summary>
        /// Retrieves all e-Visa applications for the admin.
        /// </summary>
        /// <returns>Returns a list of Application objects representing the retrieved e-Visa applications.</returns>
        public List<Application> RetrievesAllApplicationForAdmin()
        {
            Connection(); // Establish the database connection
            SqlCommand commandObject = new SqlCommand("SP_Application", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action", "SelectSP");

            sqlConnectionObject.Open();
            SqlDataReader reader = commandObject.ExecuteReader();

            List<Application> applicationDetailList = new List<Application>();

            while (reader.Read())
            {
                Application applicationObject = new Application();
                applicationObject.ApplicationId = Convert.ToInt32(reader["Applicationid"]);
                applicationObject.Userid = Convert.ToInt32(reader["Userid"]);
                applicationObject.VisaType = reader["Visatype"].ToString();
                applicationObject.ApplicationDate = reader["Applicationdate"].ToString();
                applicationObject.Country = reader["Country"].ToString();
                applicationObject.PassportNumber = reader["Passportnumber"].ToString();
                applicationObject.EntryDate = reader["Entrydate"].ToString();
                applicationObject.VisaDuration = reader["VisaDuration"].ToString();
                applicationObject.Status = reader["Status"].ToString();

                // Get the physical path from the database
                string physicalPath = reader["MedicalCertificate"].ToString();

                // Get the physical path of the application's root directory
                string rootPath = HttpContext.Current.Server.MapPath("~");

                // Remove the application's root directory from the physical path
                string relativePath = physicalPath.Replace(rootPath, string.Empty);

                // Create the relative virtual path
                string virtualPath = "~/" + relativePath;

                applicationObject.MedicalCertificate = virtualPath;
                applicationDetailList.Add(applicationObject);
            }
            reader.Close();
            sqlConnectionObject.Close();

            // Throw an exception if the retrieved list is empty
            if (applicationDetailList.Count == 0)
            {
                throw new Exception("Current you are not applied any application.fill this application");
            }
            else
            {
                return applicationDetailList;
            }

        }




        /// <summary>
        /// Retrieves all e-Visa applications for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose applications should be retrieved.</param>
        /// <returns>Returns a list of Application objects representing the user's e-Visa applications.</returns>
        public List<Application> RetrivesApplicationByUserIdForUser(int userId)
        {
            Connection(); // Establish the database connection

            SqlCommand commandObject = new SqlCommand("SP_Application", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action","SelectByUserIdSP");
            commandObject.Parameters.AddWithValue("@Userid", userId);

            sqlConnectionObject.Open();
            SqlDataReader reader = commandObject.ExecuteReader();

            List<Application> applicationDetailList = new List<Application>();

            while (reader.Read())
            {
                Application applicationObject = new Application();

                // Retrieve the application details from the database reader
                applicationObject.ApplicationId = Convert.ToInt32(reader["Applicationid"]);
                applicationObject.VisaType = reader["Visatype"].ToString();
                applicationObject.ApplicationDate = reader["Applicationdate"].ToString();
                applicationObject.Country = reader["Country"].ToString();
                applicationObject.PassportNumber = reader["Passportnumber"].ToString();
                applicationObject.EntryDate = reader["Entrydate"].ToString();
                applicationObject.VisaDuration = reader["VisaDuration"].ToString();
                applicationObject.Status = reader["Status"].ToString();

                // Get the physical path of the medical certificate from the database
                string physicalPath = reader["MedicalCertificate"].ToString();

                // Get the physical path of the application's root directory
                string rootPath = HttpContext.Current.Server.MapPath("~");

                // Remove the application's root directory from the physical path
                string relativePath = physicalPath.Replace(rootPath, string.Empty);

                // Create the relative virtual path of the medical certificate
                string virtualPath = "~/" + relativePath;

                // Set the virtual path of the medical certificate in the application object
                applicationObject.MedicalCertificate = virtualPath;

                // Add the application object to the list
                applicationDetailList.Add(applicationObject);
            }

            reader.Close();
            sqlConnectionObject.Close();

            // Throw an exception if the retrieved list is empty
            if (applicationDetailList.Count == 0)
            {
                throw new Exception("Current you are not applied any application.fill this application");
            }
            else
            {
                return applicationDetailList;
            }
        }




        public List<Application> RetrivesApplicationByApplicationId(int applicationId)
        {
            Connection(); // Establish the database connection

            SqlCommand commandObject = new SqlCommand("SPSById_Application", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@ApplicationId", applicationId);

            sqlConnectionObject.Open();
            SqlDataReader reader = commandObject.ExecuteReader();

            List<Application> applicationDetailList = new List<Application>();

            while (reader.Read())
            {
                Application applicationObject = new Application();
                applicationObject.ApplicationId = Convert.ToInt32(reader["Applicationid"]);
                applicationObject.VisaType = reader["Visatype"].ToString();
                applicationObject.ApplicationDate = reader["ApplicationDate"].ToString();
                applicationObject.Status = reader["Status"].ToString();
                applicationObject.PassportNumber = reader["Passportnumber"].ToString();
                applicationObject.EntryDate = reader["Entrydate"].ToString();
                applicationObject.VisaDuration = reader["VisaDuration"].ToString();
                applicationObject.Country = reader["Country"].ToString();

                // Get the physical path of the medical certificate from the database
                string medicalCertificatePhysicalPath = reader["MedicalCertificate"].ToString();

                // Get the physical path of the application's root directory
                string rootPath = HttpContext.Current.Server.MapPath("~");

                // Remove the application's root directory from the physical path
                string relativePath = medicalCertificatePhysicalPath.Replace(rootPath, string.Empty);

                // Create the relative virtual path of the medical certificate
                string virtualPath = "~/" + relativePath;

                // Set the virtual path of the medical certificate in the application object
                applicationObject.MedicalCertificate = virtualPath;

                User user = new User();
                user.ID = Convert.ToInt32(reader["UserId"]);
                user.Firstname = reader["Firstname"].ToString();
                user.Lastname = reader["Lastname"].ToString();
                user.Gender = reader["Gender"].ToString();
                user.Dateofbirth = reader["Dateofbirth"].ToString();
                user.Email = reader["Email"].ToString();
                user.Phonenumber = reader["Phonenumber"].ToString();
                user.Address = reader["Address"].ToString();
                user.State = reader["State"].ToString();
                user.City = reader["City"].ToString();
                user.Username = reader["Username"].ToString();
                user.Password = reader["Password"].ToString();
                user.ConfirmPassword = reader["ConfirmPassword"].ToString();

                // Get the physical path from the database
                string imagePathPhysicalPath = reader["ImagePath"].ToString();

                // Remove the application's root directory from the physical path
                string relativeImagePath = imagePathPhysicalPath.Replace(rootPath, string.Empty);

                // Create the relative virtual path
                string virtualImagePath = "~/" + relativeImagePath;

                user.ImagePath = virtualImagePath; // Set the image path of the User object

                applicationObject.User = user;
                applicationDetailList.Add(applicationObject);
            }

            reader.Close();
            sqlConnectionObject.Close();

            // Throw an exception if the retrieved list is empty
            if (applicationDetailList.Count == 0)
            {
                throw new Exception("Application not found");
            }
            else
            {
                return applicationDetailList;
            }
        }







        /// <summary>
        /// Retrieves all e-Visa applications with a specific status for administrators.
        /// </summary>
        /// <param name="status">The status value to filter the applications.</param>
        /// <returns>Returns a list of Application objects representing e-Visa applications with the specified status.</returns>
        public List<Application> RetrivesApplicationBystatusForAdmin(string status)
        {
            Connection(); // Establish the database connection

            SqlCommand commandObject = new SqlCommand("SP_Application", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action", "SelectByStatusSP");
            commandObject.Parameters.AddWithValue("@StatusValue", status); // Add the status parameter

            sqlConnectionObject.Open();
            SqlDataReader reader = commandObject.ExecuteReader();

            List<Application> applicationDetailList = new List<Application>();

            while (reader.Read())
            {
                Application applicationObject = new Application();

                // Retrieve the application details from the database reader
                applicationObject.ApplicationId = Convert.ToInt32(reader["Applicationid"]);
                applicationObject.Userid = Convert.ToInt32(reader["Userid"]);
                applicationObject.VisaType = reader["Visatype"].ToString();
                applicationObject.ApplicationDate = reader["Applicationdate"].ToString();
                applicationObject.Country = reader["Country"].ToString();
                applicationObject.PassportNumber = reader["Passportnumber"].ToString();
                applicationObject.EntryDate = reader["Entrydate"].ToString();
                applicationObject.VisaDuration = reader["VisaDuration"].ToString();
                applicationObject.Status = reader["Status"].ToString();

                // Get the physical path of the medical certificate from the database
                string physicalPath = reader["MedicalCertificate"].ToString();

                // Get the physical path of the application's root directory
                string rootPath = HttpContext.Current.Server.MapPath("~");

                // Remove the application's root directory from the physical path
                string relativePath = physicalPath.Replace(rootPath, string.Empty);

                // Create the relative virtual path of the medical certificate
                string virtualPath = "~/" + relativePath;

                // Set the virtual path of the medical certificate in the application object
                applicationObject.MedicalCertificate = virtualPath;

                // Add the application object to the list
                applicationDetailList.Add(applicationObject);
            }

            reader.Close();
            sqlConnectionObject.Close();
           
                return applicationDetailList;
            
            
        }





        /// <summary>
        /// Updates the status of an e-Visa application by the admin.
        /// </summary>
        /// <param name="applicationId">The ID of the application to update.</param>
        /// <param name="status">The new status value.</param>
        /// <returns>Returns true if the application status is successfully updated; otherwise, throws an exception.</returns>
        public bool UpdateApplicationStatusByAdmin(int applicationId, string status)
        {
            Connection(); // Establish the database connection
            sqlConnectionObject.Open();

            SqlCommand commandObject = new SqlCommand("SP_Application", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;

            // Set the parameters for the stored procedure
            commandObject.Parameters.AddWithValue("@Action","UpdateStatusSP");
            commandObject.Parameters.AddWithValue("@ApplicationId", applicationId);
            commandObject.Parameters.AddWithValue("@StatusValue", status);

            // Execute the update command and get the number of affected rows
            int result = commandObject.ExecuteNonQuery();

            sqlConnectionObject.Close();

            if (result == 0)
            {
                // If no rows are affected, throw an exception indicating the failure to update the status
                throw new Exception("Failed to update application status.");
            }
            else
            {
                // Return true to indicate the successful update of the application status
                return true;
            }
        }




        







        /// <summary>
        /// Deletes an application by its ID.
        /// </summary>
        /// <param name="applicationId">The ID of the application to delete.</param>
        /// <returns>True if the application was deleted successfully, false otherwise.</returns>
        /// <exception cref="Exception">Thrown when the deletion of the application fails.</exception>
        public bool DeleteApplicationByUser(int applicationId)
        {
            Connection();
            sqlConnectionObject.Open();
            SqlCommand commandObject = new SqlCommand("SP_Application", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action","DeleteSP");
            commandObject.Parameters.AddWithValue("@Applicationid", applicationId);
            int result = commandObject.ExecuteNonQuery();
            sqlConnectionObject.Close();
            if (result == 0)
            {
                throw new Exception("Failed to delete application.");
            }
            else
            {
                return true;
            }
        }






        /// <summary>
        /// Adds a country by the administrator.
        /// </summary>
        /// <param name="country">The country object containing the country information to be added.</param>
        /// <exception cref="Exception">Thrown if the country already exists.</exception>
        public void AddCountryByAdmin(Country country)
        {
            Connection();
            sqlConnectionObject.Open();

            // Check if the country already exists
            SqlCommand checkCommand = new SqlCommand("SP_Country", sqlConnectionObject);
            checkCommand.CommandType = CommandType.StoredProcedure;
            checkCommand.Parameters.AddWithValue("@Action", "CheckExsistSP");
            checkCommand.Parameters.AddWithValue("@CountryName", country.CountryName);
            int existingCount = (int)checkCommand.ExecuteScalar();

            if (existingCount > 0)
            {
                throw new Exception("Country already exists."); // Raise an exception if the country already exists
            }

            // Add the country if it doesn't exist
            SqlCommand command = new SqlCommand("SP_Country", sqlConnectionObject);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Action", "InsertSP");
            command.Parameters.AddWithValue("@CountryValue", country.CountryName);
            command.Parameters.AddWithValue("@CountryName", country.CountryName);
            command.ExecuteNonQuery();

            sqlConnectionObject.Close();
        }







        /// <summary>
        /// Retrieves all countries for the administrator.
        /// </summary>
        /// <returns>A list of Country objects representing all countries.</returns>
        /// <exception cref="Exception">Thrown if no countries are found.</exception>
        public List<Country> RetrievesAllCountriesForAdmin()
        {
            List<Country> countriesList = new List<Country>();
            Connection();
            sqlConnectionObject.Open();
            SqlCommand command = new SqlCommand("SP_Country", sqlConnectionObject);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Action", "SelectSP");
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Country country = new Country();
                country.CountryValue = reader["Countryvalue"].ToString();
                country.CountryName = reader["Countryname"].ToString();
                countriesList.Add(country);
            }
            sqlConnectionObject.Close();

            if (countriesList.Count == 0)
            {
                throw new Exception("No countries found."); // Throw an exception if no countries are retrieved
            }

            return countriesList;
        }







        /// <summary>
        /// Deletes a country by the administrator.
        /// </summary>
        /// <param name="countryValue">The value of the country to be deleted.</param>
        /// <exception cref="Exception">Thrown if the country was not deleted.</exception>
        public void DeleteCountryByAdmin(string countryValue)
        {
            Connection();
            sqlConnectionObject.Open();
            SqlCommand commandObject = new SqlCommand("SP_Country", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action", "DeleteSP");
            commandObject.Parameters.AddWithValue("@CountryValue", countryValue);
            int result = commandObject.ExecuteNonQuery();
            sqlConnectionObject.Close();
            if (result == 0)
            {
                throw new Exception("Failed to delete the country."); // Throw an exception if the country was not deleted
            }
        }








        /// <summary>
        /// Adds a new visa type by the administrator.
        /// </summary>
        /// <param name="visaType">The VisaType object representing the new visa type.</param>
        /// <exception cref="Exception">Thrown if the visa type already exists.</exception>
        public void AddVisaTypeByAdmin(Visatype visaType)
        {
            Connection();
            sqlConnectionObject.Open();

            // Check if the visa type already exists
            SqlCommand checkCommand = new SqlCommand("SP_VisaType", sqlConnectionObject);
            checkCommand.CommandType = CommandType.StoredProcedure;
            checkCommand.Parameters.AddWithValue("@Action", "CheckExsistSP");
            checkCommand.Parameters.AddWithValue("@VisatypeName", visaType.VisaTypeName);
            int existingCount = (int)checkCommand.ExecuteScalar();

            if (existingCount > 0)
            {
                throw new Exception("Visa type already exists."); // Raise an exception if the visa type already exists
            }

            SqlCommand command = new SqlCommand("SP_VisaType", sqlConnectionObject);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Action", "InsertSP");
            command.Parameters.AddWithValue("@VisatypeValue", visaType.VisaTypeName);
            command.Parameters.AddWithValue("@VisatypeName", visaType.VisaTypeName);
            command.Parameters.AddWithValue("@VisaDuration", visaType.VisaDuration);
            command.ExecuteNonQuery();

            sqlConnectionObject.Close();
        }





        /// <summary>
        /// Retrieves all visa types for the administrator.
        /// </summary>
        /// <returns>A list of Visatype objects representing all visa types.</returns>
        /// <exception cref="Exception">Thrown if no visa types are found.</exception>
        public List<Visatype> RetrievesAllVisaTypeForAdmin()
        {
            List<Visatype> visatypeList = new List<Visatype>();
            Connection();
            sqlConnectionObject.Open();
            SqlCommand command = new SqlCommand("SP_VisaType", sqlConnectionObject);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Action", "SelectSP");
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Visatype visatypeObject = new Visatype();
                visatypeObject.VisaTypeValue = reader["VisatypeValue"].ToString();
                visatypeObject.VisaTypeName = reader["VisatypeName"].ToString();
                visatypeObject.VisaDuration = reader["VisaDuration"].ToString();
                visatypeList.Add(visatypeObject);
            }
            sqlConnectionObject.Close();

            if (visatypeList.Count == 0)
            {
                throw new Exception("No visa types found."); // Throw an exception if no visa types are retrieved
            }

            return visatypeList;
        }




        /// <summary>
        /// Deletes a visa type by the administrator.
        /// </summary>
        /// <param name="visatypeValue">The value of the visa type to be deleted.</param>
        /// <exception cref="Exception">Thrown if the deletion of the visa type fails.</exception>
        public void DeleteVisaTypeByAdmin(string visatypeValue)
        {
            Connection();
            sqlConnectionObject.Open();
            SqlCommand commandObject = new SqlCommand("SP_VisaType", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action","DeleteSp");
            commandObject.Parameters.AddWithValue("@VisatypeValue", visatypeValue);
            int result = commandObject.ExecuteNonQuery();
            sqlConnectionObject.Close();
            if (result == 0)
            {
                throw new Exception("Failed to delete the visatype."); // Throw an exception if the visa type was not deleted
            }
        }


        
        public string GetVisaDuration(string  visaType)
        {
            Connection();
            sqlConnectionObject.Open(); // Open the connection
            SqlCommand commandObject = new SqlCommand("SP_VisaType", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action", "SelectVisaDurationSP");
            commandObject.Parameters.AddWithValue("@VisatypeName",visaType);
            string visaDurationMonth = commandObject.ExecuteScalar() as string;
            sqlConnectionObject.Close();
            return visaDurationMonth;
        }

    }
}
