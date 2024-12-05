using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaleManagementWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Set the form to start in the center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;


            // Disable the maximize/restore button
            this.MaximizeBox = false;

            // Optional: Set a fixed border style to prevent resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Update new code gituhub 
            // Test commit github

            string username = txb_username.Text;
            string password = txb_password.Text;

            string hashPassword = Utils.HashPassword(password);

            bool checkLogin = CheckLogin(username, hashPassword);

            if (checkLogin)
            {

                MenuForm main = new MenuForm();
                main.Show();
                this.Hide();

            }else
            {


                MessageBox.Show("Username or password is incorrect !");
            }





        }
        private bool CheckLogin(string username, string hashedPassword)
        {
            string query = "SELECT password, roleId FROM Employee WHERE username = @username";

            using (SqlConnection connection = new SqlConnection(Connection.SQLConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["password"].ToString(); // Retrieved hashed password
                            int roleId = Convert.ToInt32(reader["roleId"]); // Retrieved roleId


                            Utils.roleID = roleId; // Assign the roleId to your global variable or utility class

                            return storedHash == hashedPassword; // Compare the hashes
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }

            return false; // Return false if username not found or any error occurs
        }


        /* private bool CheckLogin(string username, string hashedPassword)
         {
             string query = "SELECT password FROM Employee WHERE username = @username";


             //Utils.roleID = id_get_from_database
             using (SqlConnection connection = new SqlConnection(Connection.SQLConnection))
             {
                 SqlCommand command = new SqlCommand(query, connection);
                 command.Parameters.AddWithValue("@username", username);

                 try
                 {
                     connection.Open();
                     object result = command.ExecuteScalar();

                     if (result != null)
                     {
                         string storedHash = result.ToString(); // Retrieved hashed password
                         return storedHash == hashedPassword;  // Compare the hashes
                     }
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show($"An error occurred: {ex.Message}");
                 }
             }

             return false; // Return false if username not found or any error occurs
         }*/
    }
}
