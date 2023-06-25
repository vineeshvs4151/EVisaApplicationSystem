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

    public class HomeRepository
    {
        private SqlConnection sqlConnectionObject;

        // <summary>
        /// The HomeRepository class handles database connection related activities.
        /// </summary>
        /// <remarks>
        /// This class provides a method to establish a connection with the database.
        /// It uses the connection string named "EvisaApplicationDBConnectionString"
        /// from the configuration file to create a SqlConnection object.
        /// </remarks>
        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["EvisaApplicationDBConnectionString"].ConnectionString;
            sqlConnectionObject = new SqlConnection(constr);
        }


        /// <summary>
        /// Adds a message submitted through a contact form to the database.
        /// </summary>
        /// <param name="contactObject">The Contact object containing the message details.</param>
        /// <returns>True if the message was successfully added, otherwise false.</returns>
        public bool AddContactFormMessageByUser(Contact contactObject)
        {
            Connection();

            // Create a SqlCommand object for the stored procedure
            SqlCommand commandObject = new SqlCommand("SP_ContactMessage", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;

            // Open the database connection
            sqlConnectionObject.Open();

            // Set the command type and add parameters to the command
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action", "InsertSP");
            commandObject.Parameters.AddWithValue("@Name", contactObject.Name);
            commandObject.Parameters.AddWithValue("@Email", contactObject.Email);
            commandObject.Parameters.AddWithValue("@Message", contactObject.Message);

            // Execute the insert operation and get the number of affected rows
            int result = commandObject.ExecuteNonQuery();

            // Close the database connection
            sqlConnectionObject.Close();

            // Check if the insert operation was successful

            if (result == 0)
            {
                // Throw an exception if the update operation failed
                throw new Exception("Failed to add contact message.");
            }
            else
            {
                return true;
            }
        }





        /// <summary>
        /// Retrieves all contact form messages entered by users for the admin.
        /// </summary>
        /// <returns>A list of Contact objects representing the contact form messages.</returns>
        public List<Contact> GetAllContactFormMessagesForAdmin()
        {
            Connection();

            // Open the database connection
            sqlConnectionObject.Open();
            SqlCommand command = new SqlCommand("SP_ContactMessage", sqlConnectionObject);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Action", "SelectSP");

            SqlDataReader reader = command.ExecuteReader();

            List<Contact> messageDetailList = new List<Contact>();

            while (reader.Read())
            {
                Contact contactObject = new Contact();

                contactObject.Id = Convert.ToInt32(reader["id"]);
                contactObject.Name = reader["Name"].ToString();
                contactObject.Email = reader["Email"].ToString();
                contactObject.Message = reader["Message"].ToString();
                contactObject.MessageStatus = reader["MessageStatus"].ToString(); // Populate MessageStatus property
                contactObject.RepliedMessage = reader["Repliedmessage"].ToString();

                messageDetailList.Add(contactObject);
            }

            reader.Close();

            return messageDetailList;
        }






        /// <summary>
        /// Retrieves contact form message details by the specified message status.
        /// </summary>
        /// <param name="messageStatus">The status of the contact form messages to retrieve.</param>
        /// <returns>A list of Contact objects containing the retrieved message details.</returns>
        public List<Contact> RetrievesContactFormMessageDetailsByStatus(string messageStatus)
        {
            Connection();

            // Open the database connection
            sqlConnectionObject.Open();

            // Create a SqlCommand object for the stored procedure
            SqlCommand command = new SqlCommand("SP_ContactMessage", sqlConnectionObject);
            command.CommandType = CommandType.StoredProcedure;

            // Set the stored procedure parameters
            command.Parameters.AddWithValue("@Action", "SelectByStatusSP");
            command.Parameters.AddWithValue("@Messagestatus", messageStatus); // Pass the messageStatus parameter

            // Execute the stored procedure and retrieve the results
            SqlDataReader reader = command.ExecuteReader();

            // Create a list to store the retrieved message details
            List<Contact> messageDetailList = new List<Contact>();

            // Read the results and populate the messageDetailList
            while (reader.Read())
            {
                // Create a new Contact object to hold the retrieved details
                Contact contactObject = new Contact();

                // Populate the Contact object with data from the SqlDataReader
                contactObject.Id = Convert.ToInt32(reader["id"]);
                contactObject.Name = reader["Name"].ToString();
                contactObject.Email = reader["Email"].ToString();
                contactObject.Message = reader["Message"].ToString();
                contactObject.MessageStatus = reader["Messagestatus"].ToString();
                contactObject.RepliedMessage = reader["Repliedmessage"].ToString();

                // Add the Contact object to the messageDetailList
                messageDetailList.Add(contactObject);
            }

            // Close the SqlDataReader
            reader.Close();
          
           return messageDetailList;
        }








        /// <summary>
        /// Retrieves contact form message details from the database based on the specified message ID.
        /// </summary>
        /// <param name="messageId">The ID of the message to retrieve.</param>
        /// <returns>A list of Contact objects containing the contact details for the specified message.</returns>
        public List<Contact> RetrievesContactFormMessageDetailsById(int messageId)
        {
            Connection();

            // Create a SqlCommand object for the stored procedure
            SqlCommand commandObject = new SqlCommand("SP_ContactMessage", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action", "SelectByIdSP");
            commandObject.Parameters.AddWithValue("@id", messageId);

            sqlConnectionObject.Open();
            SqlDataReader reader = commandObject.ExecuteReader();

            List<Contact> contactDetailList = new List<Contact>();

            while (reader.Read())
            {
                Contact contactObject = new Contact();

                // Set the properties of the Contact object from the data reader
                contactObject.Id = Convert.ToInt32(reader["id"]);
                contactObject.Name = reader["Name"].ToString();
                contactObject.Email = reader["Email"].ToString();
                contactObject.Message = reader["Message"].ToString();

                // Add the Contact object to the list
                contactDetailList.Add(contactObject);
            }

            reader.Close();
            sqlConnectionObject.Close();

            return contactDetailList;
        }





        /// <summary>
        /// Deletes the contact form message details with the specified message ID.
        /// </summary>
        /// <param name="messageId">The ID of the contact form message to delete.</param>
        /// <returns>True if the deletion operation is successful; otherwise, false.</returns>
        public bool DeleteContactFormMessageDetailsByAdmin(int messageId)
        {
            Connection();

            // Create a SqlCommand object for the stored procedure
            SqlCommand commandObject = new SqlCommand("SP_ContactMessage", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;

            // Set the stored procedure parameters
            commandObject.Parameters.AddWithValue("@Action", "DeleteSP");
            commandObject.Parameters.AddWithValue("@id", messageId);

            // Open the database connection
            sqlConnectionObject.Open();

            // Execute the delete operation and get the number of affected rows
            int result = commandObject.ExecuteNonQuery();

            // Close the database connection
            sqlConnectionObject.Close();

            // Check if the delete operation was successful
            if (result == 0)
            {
                // Throw an exception if the delete operation failed
                throw new Exception("Failed to delete contact message.");
            }
            else
            {
                // Return true to indicate successful deletion
                return true;
            }
        }





        /// <summary>
        /// Adds an admin reply to the contact form message with the specified message ID.
        /// </summary>
        /// <param name="contactObject">The Contact object containing the admin reply message.</param>
        /// <param name="messageId">The ID of the contact form message.</param>
        /// <returns>True if the admin reply is successfully added; otherwise, false.</returns>
        public bool AddAdminReplyToContactFormMessage(Contact contactObject, int messageId)
        {
            Connection();

            // Open the database connection
            sqlConnectionObject.Open();

            // Create a SqlCommand object for the stored procedure
            SqlCommand commandObject = new SqlCommand("SP_ContactMessage", sqlConnectionObject);
            commandObject.CommandType = CommandType.StoredProcedure;
            commandObject.Parameters.AddWithValue("@Action", "UpdateSP");
            commandObject.Parameters.AddWithValue("@Repliedmessage", contactObject.RepliedMessage);
            commandObject.Parameters.AddWithValue("@id", messageId);

            if (contactObject.RepliedMessage != null)
            {
                // Execute the update operation and get the number of affected rows
                commandObject.ExecuteNonQuery();

                // Close the database connection
                sqlConnectionObject.Close();

                // Return true to indicate successful addition of the admin reply
                return true;
            }
            else
            {
                // Throw an exception if the admin reply message is null
                throw new Exception("Failed to reply to the contact message. Admin reply message is required.");
            }
        }



    }
}