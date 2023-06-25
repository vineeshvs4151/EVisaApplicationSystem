
using EVisaApplicationSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EVisaApplicationSystem.Repository
{
    public class UserRepository
    {
        private SqlConnection sqlConnectionObject;

        /// <summary>
        /// Establishes a database connection.
        /// </summary>
        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["EvisaApplicationDBConnectionString"].ConnectionString;
            sqlConnectionObject = new SqlConnection(constr);
        }




        /// <summary>
        /// Inserts a user into the database.
        /// </summary>
        /// <param name="user">The user object to insert.</param>
        /// <param name="imagePath">The image path of the user.</param>
        /// <exception cref="Exception">Thrown when the username already exists.</exception>
        public void InsertUser(User user, string imagePath)
        {
            Connection();

            // Check if the visa type already exists
            sqlConnectionObject.Open();

            SqlCommand commandObject = new SqlCommand("SP_User", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action", "InsertSP");
            commandObject.Parameters.AddWithValue("@Firstname", user.Firstname);
            commandObject.Parameters.AddWithValue("@Lastname", user.Lastname);
            commandObject.Parameters.AddWithValue("@Gender", user.Gender);
            commandObject.Parameters.AddWithValue("@Dateofbirth", user.Dateofbirth);
            commandObject.Parameters.AddWithValue("@Email", user.Email);
            commandObject.Parameters.AddWithValue("@Phonenumber", user.Phonenumber);
            commandObject.Parameters.AddWithValue("@Address", user.Address);
            commandObject.Parameters.AddWithValue("@State", user.State);
            commandObject.Parameters.AddWithValue("@City", user.City);
            commandObject.Parameters.AddWithValue("@Username", user.Username);
            commandObject.Parameters.AddWithValue("@ImagePath", imagePath);

            // Encrypt the password before saving
            string encryptedPassword = EncryptPassword(user.Password);
            commandObject.Parameters.AddWithValue("@Password", encryptedPassword);
            string encryptedConfirmPassword = EncryptConfirmPassword(user.ConfirmPassword);
            commandObject.Parameters.AddWithValue("@ConfirmPassword", encryptedConfirmPassword);


            // Check if the username already exists
            if (CheckUsernameExists(user.Username))
            {

                throw new Exception("Username already exists.");

            }
            else if (CheckEmailExists(user.Email))
            {
                throw new Exception("Email already exists.");
            }
            else if (CheckPhoneNumberExists(user.Phonenumber))
            {
                throw new Exception("Phone number already exists.");
            }

            else
            {
                commandObject.ExecuteNonQuery();
                sqlConnectionObject.Close();
            }
        }





        /// <summary>
        /// Checks if a username already exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username exists, otherwise false.</returns>
        public bool CheckUsernameExists(string username)
        {
            Connection();
            sqlConnectionObject.Open();

            SqlCommand checkCommand = new SqlCommand("SP_User", sqlConnectionObject);
            checkCommand.CommandType = CommandType.StoredProcedure;
            checkCommand.Parameters.AddWithValue("@Action", "CheckUsernameExistSP");
            checkCommand.Parameters.AddWithValue("@Username", username);
            int existingCount = (int)checkCommand.ExecuteScalar();

            if (existingCount > 0)
            {
                return true;
            }
          
            return false;
        }





        public bool CheckEmailExists(string email)
        {
            Connection();
            sqlConnectionObject.Open();

            SqlCommand checkCommand = new SqlCommand("SP_User", sqlConnectionObject);
            checkCommand.CommandType = CommandType.StoredProcedure;
            checkCommand.Parameters.AddWithValue("@Action", "CheckEmailExistSP");
            checkCommand.Parameters.AddWithValue("@Email", email);
            int existingCount = (int)checkCommand.ExecuteScalar();

            if (existingCount > 0)
            {
                return true;
            }
            sqlConnectionObject.Close();
            return false;
        }




        public bool CheckPhoneNumberExists(string phonenumber)
        {
            Connection();
            sqlConnectionObject.Open();
            SqlCommand checkCommand = new SqlCommand("SP_User", sqlConnectionObject);
            checkCommand.CommandType = CommandType.StoredProcedure;
            checkCommand.Parameters.AddWithValue("@Action", "CheckPhoneNumberExistSP");
            checkCommand.Parameters.AddWithValue("@Phonenumber", phonenumber);

            object result = checkCommand.ExecuteScalar();
            int existingCount = result != null ? Convert.ToInt32(result) : 0;

            if (existingCount > 0)
            {
               
                return true;
            }

            sqlConnectionObject.Close();
            return false;
        }








        /// <summary>
        /// Encrypts the given password using SHA256 hashing algorithm.
        /// </summary>
        /// <param name="password">The password to encrypt.</param>
        /// <returns>The encrypted password.</returns>
        private string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }




        /// <summary>
        /// Encrypts the given confirm password using SHA256 hashing algorithm.
        /// </summary>
        /// <param name="password">The confirm password to encrypt.</param>
        /// <returns>The encrypted confirm password.</returns>
        private string EncryptConfirmPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }








        /// <summary>
        /// Retrieves user details by user ID from the database.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of User objects containing the user details.</returns>
        public List<User> GetUserById(int userId)
        {
            Connection(); // Establishes a database connection

            SqlCommand commandObject = new SqlCommand("SP_User", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Userid", userId);
            commandObject.Parameters.AddWithValue("@Action", "SelectByUserIdSP");
            sqlConnectionObject.Open(); // Opens the database connection

            SqlDataReader reader = commandObject.ExecuteReader(); // Executes the database query

            List<User> UserDetailList = new List<User>(); // Create a list to store the user details

            while (reader.Read()) // Iterate through the result set
            {
                User userObject = new User(); // Create a new User object for each row in the result set

                // Retrieve user details from the result set and assign them to the User object properties
                userObject.ID = Convert.ToInt32(reader["ID"]);
                userObject.Firstname = reader["Firstname"].ToString();
                userObject.Lastname = reader["Lastname"].ToString();
                userObject.Gender = reader["Gender"].ToString();
                userObject.Dateofbirth = reader["Dateofbirth"].ToString();
                userObject.Email = reader["Email"].ToString();
                userObject.Phonenumber = reader["Phonenumber"].ToString();
                userObject.Address = reader["Address"].ToString();
                userObject.State = reader["State"].ToString();
                userObject.City = reader["City"].ToString();
                userObject.Username = reader["Username"].ToString();
                userObject.Password = reader["Password"].ToString();

                // Get the physical path from the database
                string physicalPath = reader["ImagePath"].ToString();

                // Get the physical path of the application's root directory
                string rootPath = HttpContext.Current.Server.MapPath("~");

                // Remove the application's root directory from the physical path
                string relativePath = physicalPath.Replace(rootPath, string.Empty);

                // Create the relative virtual path
                string virtualPath = "~/" + relativePath;

                userObject.ImagePath = virtualPath; // Set the image path of the User object

                UserDetailList.Add(userObject); // Add the User object to the list
            }
            if (UserDetailList.Count == 0)
            {
                throw new Exception("Error found"); // Throw an exception if no user details are retrieved
            }
            reader.Close(); // Close the data reader
            sqlConnectionObject.Close();
            return UserDetailList; // Return the list of user details
        }







        /// <summary>
        /// Updates the user profile with the provided user details and image path.
        /// </summary>
        /// <param name="user">The User object containing the updated user details.</param>
        /// <param name="imagePath">The image path for the user's profile picture.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>True if the profile update is successful; otherwise, false.</returns>
        public void UpdateUserProfileByUser(User user, string imagePath, int userId)
        {
            Connection(); // Establishes a database connection

            sqlConnectionObject.Open(); // Open the connection

            SqlCommand commandObject = new SqlCommand("SP_User", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action", "UpdateSP");
            commandObject.Parameters.AddWithValue("@Userid", userId);
            commandObject.Parameters.AddWithValue("@Firstname", user.Firstname);
            commandObject.Parameters.AddWithValue("@Lastname", user.Lastname);
            commandObject.Parameters.AddWithValue("@Gender", user.Gender);
            commandObject.Parameters.AddWithValue("@Dateofbirth", user.Dateofbirth);
            commandObject.Parameters.AddWithValue("@Email", user.Email);
            commandObject.Parameters.AddWithValue("@Phonenumber", user.Phonenumber);
            commandObject.Parameters.AddWithValue("@Address", user.Address);
            commandObject.Parameters.AddWithValue("@State", user.State);
            commandObject.Parameters.AddWithValue("@City", user.City);
            commandObject.Parameters.AddWithValue("@Username", user.Username);
            commandObject.Parameters.AddWithValue("@ImagePath", imagePath);

             commandObject.ExecuteNonQuery(); // Execute the database update query

            sqlConnectionObject.Close(); // Close the connection

        }





        /// <summary>
        /// Retrieves the image path of a user with the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The image path of the user.</returns>
        public string GetUserImagePath(int userId)
        {
            Connection();
            sqlConnectionObject.Open(); // Open the connection

            SqlCommand commandObject = new SqlCommand("SP_User", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure; // Specify that it's a stored procedure
            commandObject.Parameters.AddWithValue("@Userid", userId);
            commandObject.Parameters.AddWithValue("@Action", "SelectImagePathSP");
            string imagePath = commandObject.ExecuteScalar() as string; 

            sqlConnectionObject.Close();

            return imagePath;
        }



        /// <summary>
        /// Deletes the user profile with the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        public void DeleteUserProfileByUser(int userId)
        {
            Connection();
            sqlConnectionObject.Open(); // Open the connection

            SqlCommand commandObject = new SqlCommand("SP_User", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure; // Specify that it's a stored procedure
            commandObject.Parameters.AddWithValue("@Userid", userId);
            commandObject.Parameters.AddWithValue("@Action", "DeleteSp");
            commandObject.ExecuteNonQuery(); // Use ExecuteNonQuery instead of ExecuteReader

            sqlConnectionObject.Close();
        }



        /// <summary>
        /// Retrieves a list of states.
        /// </summary>
        /// <returns>A list of <see cref="States"/> objects representing the states.</returns>
        public List<States> GetStates()
        {
            List<States> statesList = new List<States>();
            Connection();
            sqlConnectionObject.Open();
            SqlCommand command = new SqlCommand("SP_States", sqlConnectionObject);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Action", "SelectSP");
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                States statesObject = new States();
                statesObject.Statesvalue = reader["Statevalue"].ToString();
                statesObject.Statesname = reader["Statename"].ToString();
                statesList.Add(statesObject);
            }
            sqlConnectionObject.Close();

            if (statesList.Count == 0)
            {
                throw new Exception("No states found.");
            }
            return statesList;
        }






        /// <summary>
        /// Retrieves a list of cities based on the specified state value.
        /// </summary>
        /// <param name="statesValue">The state value to filter the cities.</param>
        /// <returns>A list of <see cref="Cities"/> objects representing the cities.</returns>
        public List<Cities> GetCitiesByStates(string statesValue)
        {
            List<Cities> citiesList = new List<Cities>();
            Connection();
            sqlConnectionObject.Open();
            SqlCommand command = new SqlCommand("SP_Cities", sqlConnectionObject);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Action", "SelectSP");
            command.Parameters.AddWithValue("@Statevalue", statesValue);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                
                Cities citiesObject = new Cities();
                citiesObject.Cityvalue = reader["Cityvalue"].ToString();
                citiesObject.Cityname = reader["Cityname"].ToString();
                citiesObject.Statesvalue = reader["Statevalue"].ToString();
                citiesList.Add(citiesObject);
            }
            sqlConnectionObject.Close();
           
            return citiesList;
        }






        /// <summary>
        /// Adds a new state to the database.
        /// </summary>
        /// <param name="statesObject">The <see cref="States"/> object representing the state to be added.</param>
        /// <exception cref="Exception">Thrown if the country name already exists.</exception>
        public void AddStatesByAdmin(States statesObject)
        {
            Connection();
            sqlConnectionObject.Open();

          
            SqlCommand checkCommand = new SqlCommand("SP_States", sqlConnectionObject);
            checkCommand.CommandType = CommandType.StoredProcedure;
            checkCommand.Parameters.AddWithValue("@Action", "CheckExistSP");
            checkCommand.Parameters.AddWithValue("@Statename", statesObject.Statesname);
            int existingCount = (int)checkCommand.ExecuteScalar();

            if (existingCount > 0)
            {
                throw new Exception("Country name already exists.");
            }

            SqlCommand command = new SqlCommand("StatesCRUDSP", sqlConnectionObject);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Action", "InsertSP");
            command.Parameters.AddWithValue("@Statevalue", statesObject.Statesname);
            command.Parameters.AddWithValue("@Statename", statesObject.Statesname);
            command.ExecuteNonQuery();
            sqlConnectionObject.Close();
        }





        /// <summary>
        /// Adds a new city to the database.
        /// </summary>
        /// <param name="citiesObject">The <see cref="Cities"/> object representing the city to be added.</param>
        /// <exception cref="Exception">Thrown if the city name already exists.</exception>
        public void AddCitiesByAdmin(Cities citiesObject)
        {
            Connection();
            sqlConnectionObject.Open();


            SqlCommand checkCommand = new SqlCommand("SP_Cities", sqlConnectionObject);
            checkCommand.CommandType = CommandType.StoredProcedure;
            checkCommand.Parameters.AddWithValue("@Action", "CheckExistSP");
            checkCommand.Parameters.AddWithValue("@Cityname", citiesObject.Cityname);
            int existingCount = (int)checkCommand.ExecuteScalar();

            if (existingCount > 0) 
            {
                throw new Exception("City name already exists.");
            }


            SqlCommand command = new SqlCommand("CitiesCRUDSP", sqlConnectionObject);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Action", "InsertSP");
            command.Parameters.AddWithValue("@Cityvalue", citiesObject.Cityname);
            command.Parameters.AddWithValue("@Cityname", citiesObject.Cityname);
            command.Parameters.AddWithValue("@Statevalue", citiesObject.Statesvalue);
            command.ExecuteNonQuery();
            sqlConnectionObject.Close();
        }




    }
}
