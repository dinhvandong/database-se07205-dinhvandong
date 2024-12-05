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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SaleManagementWinform
{
    public partial class AddPurchaseHistory : Form
    {

        string[] items = { "Cancle", "Pending",  "Finish" };
        static int selectID = 0;

        public AddPurchaseHistory()
        {
            InitializeComponent();
            cbx_status.Items.AddRange(items);
            cbx_status.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = cbx_status.SelectedIndex; // Gets the selected index
            if (selectedIndex != -1) // Check if an item is selected
            {
                MessageBox.Show($"Selected Index: {selectedIndex}");
            }
            else
            {
                MessageBox.Show("No item selected.");
            }
        }


        private void InsertDataToPurchaseHistory(int purchaseID, string customerCode, 
            string productCode, DateTime purchaseDate, int quantity, int status, int active)
        {
            // SQL query to insert data into PurchaseHistory
            string query = "INSERT INTO PurchaseHistory " +
                "(purchaseID, customerCode, productCode, purchaseDate, quantity, status, active) " +
                "VALUES (@purchaseID, @customerCode, @productCode, @purchaseDate, @quantity, @status, @active)";

            using (SqlConnection connection = new SqlConnection(Connection.SQLConnection))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Create the SQL command
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@purchaseID", purchaseID);
                        command.Parameters.AddWithValue("@customerCode", customerCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@productCode", productCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@purchaseDate", purchaseDate);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@status", status);
                        command.Parameters.AddWithValue("@active", active);

                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} row(s) inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            int purchaseID = int.Parse(tBx_purchaseID.Text.ToString());
            string productID = (tBx_productID.Text.ToString());
            string customerID = (tBx_customerID.Text.ToString());

            int quantity = int.Parse(tBx_quantity.Text.ToString());
            DateTime selectedDate = dtp_datetime.Value.Date;

            InsertDataToPurchaseHistory(purchaseID, customerID, productID, selectedDate, quantity, selectID, 1);




        }
    }
}
