 // LoginRepository.cs
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using EVisaApplicationSystem.Models;

namespace EVisaApplicationSystem.Repository
{
    public class LoginRepository
    {
        private SqlConnection sqlConnectionObject;

        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["EvisaApplicationDBConnectionString"].ConnectionString;
            sqlConnectionObject = new SqlConnection(constr);
        }






        /// <summary>
        /// Authenticates the admin using the provided login credentials.
        /// </summary>
        /// <param name="loginModel">The LoginModel object containing the login credentials.</param>
        /// <param name="loginAdminId">The output parameter that receives the loginAdminId upon successful authentication.</param>
        /// <returns>True if the authentication is successful; otherwise, false.</returns>
        public bool AuthenticateAdmin(LoginModel loginModel, out string loginAdminId)
        {


            // Use the SqlConnection class to establish a connection to the database
            Connection();
            sqlConnectionObject.Open();
            // Use the SqlCommand class to execute the stored procedure for admin login
            SqlCommand command = new SqlCommand("SP_AdminLogin", sqlConnectionObject);
            command.CommandType = CommandType.StoredProcedure;

            // Set the input parameters for the stored procedure
            command.Parameters.AddWithValue("@Username", loginModel.Username);
            string encryptedPassword = AdminEncryptPassword(loginModel.Password);
            command.Parameters.AddWithValue("@Password", encryptedPassword);

            // Set the output parameter to retrieve the loginAdminId
            command.Parameters.Add("@loginAdminId", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            // Execute the stored procedure
            command.ExecuteNonQuery();

            // Retrieve the loginAdminId from the output parameter
            loginAdminId = Convert.ToString(command.Parameters["@loginAdminId"].Value);

            // Check if the login is successful
            return !string.IsNullOrEmpty(loginAdminId) && loginAdminId != "0";

        }







        /// <summary>
        /// Authenticates the user using the provided login credentials.
        /// </summary>
        /// <param name="loginModel">The LoginModel object containing the login credentials.</param>
        /// <param name="loginUserId">The output parameter that receives the loginUserId upon successful authentication.</param>
        /// <returns>True if the authentication is successful; otherwise, false.</returns>
        public bool AuthenticateUser(LoginModel loginModel, out string loginUserId)
        {

            // Use the SqlConnection class to establish a connection to the database
            Connection();
            sqlConnectionObject.Open();
            // Use the SqlCommand class to execute the stored procedure for user login
            SqlCommand command = new SqlCommand("SP_UserLogin", sqlConnectionObject);

            command.CommandType = CommandType.StoredProcedure;

            // Set the input parameters for the stored procedure
            command.Parameters.AddWithValue("@Username", loginModel.Username);
            string encryptedPassword = EncryptPassword(loginModel.Password);
            command.Parameters.AddWithValue("@Password", encryptedPassword);
            command.Parameters.Add("@Loginuserid", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            // Execute the stored procedure
            command.ExecuteNonQuery();

            // Retrieve the loginUserId from the output parameter
            loginUserId = Convert.ToString(command.Parameters["@Loginuserid"].Value);

            // Check if the login is successful
            return !string.IsNullOrEmpty(loginUserId) && loginUserId != "0";
        }






        /// <summary>
        /// Encrypts the given password using the SHA256 hashing algorithm.
        /// </summary>
        /// <param name="password">The password to be encrypted.</param>
        /// <returns>The encrypted password as a string.</returns>
        private string EncryptPassword(string password)
        {
           

            // Use the SHA256 class to create a SHA256 hashing object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to bytes
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash value of the password bytes
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert the hash bytes to a base64 string and return it
                return Convert.ToBase64String(hash);
            }
        }





        /// <summary>
        /// Encrypts the given password using the SHA256 hashing algorithm for admin users.
        /// </summary>
        /// <param name="password">The password to be encrypted.</param>
        /// <returns>The encrypted password as a string.</returns>
        private string AdminEncryptPassword(string password)
        {
           

            // Use the SHA256 class to create a SHA256 hashing object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to bytes
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash value of the password bytes
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert the hash bytes to a base64 string and return it
                return Convert.ToBase64String(hash);
            }
        }

    }
}
