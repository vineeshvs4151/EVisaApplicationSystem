using EVisaApplicationSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EVisaApplicationSystem.Repository
{
    /// <summary>
    /// Repository class responsible for handling admin-related database operations.
    /// </summary>
    public class AdminRepository
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
        /// Inserts an admin into the database.
        /// </summary>
        /// <param name="adminObject">The admin object to insert.</param>
        /// <param name="imagePath">The image path of the admin.</param>
        /// <returns>True if the admin is successfully inserted, otherwise false.</returns>
        /// <exception cref="Exception">Thrown when the form is not filled with valid details.</exception>
        public bool InsertAdmin(Admin adminObject, string imagePath)
        {
            Connection();
            SqlCommand commandObject = new SqlCommand("SP_InsertAdmin", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            sqlConnectionObject.Open();

            commandObject.CommandType = CommandType.StoredProcedure;
            // Add other parameters to the command
            commandObject.Parameters.AddWithValue("@Firstname", adminObject.Firstname);
            commandObject.Parameters.AddWithValue("@Lastname", adminObject.Lastname);
            commandObject.Parameters.AddWithValue("@Gender", adminObject.Gender);
            commandObject.Parameters.AddWithValue("@Dateofbirth", adminObject.Dateofbirth);
            commandObject.Parameters.AddWithValue("@Email", adminObject.Email);
            commandObject.Parameters.AddWithValue("@Phonenumber", adminObject.Phonenumber);
            commandObject.Parameters.AddWithValue("@Address", adminObject.Address);
            commandObject.Parameters.AddWithValue("@State", adminObject.State);
            commandObject.Parameters.AddWithValue("@City", adminObject.City);
            commandObject.Parameters.AddWithValue("@Username", adminObject.Username);
            commandObject.Parameters.AddWithValue("@ImagePath", imagePath);
            // Encrypt the password before saving
            string encryptedPassword = EncryptPassword(adminObject.Password);
            commandObject.Parameters.AddWithValue("@Password", encryptedPassword);
            int result = commandObject.ExecuteNonQuery();
            sqlConnectionObject.Close();

            if (result == 0)
            {
                throw new Exception("Fill the form with valid details.");
            }
            else
            {
                return true;
            }
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



    }
}