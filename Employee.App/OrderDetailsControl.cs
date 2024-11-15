//using System;
//using System.Drawing;
//using System.Windows.Forms;
//using static System.Windows.Forms.DataFormats;

//namespace Mocktails.Employee.App
//{
//    public class OrderDetailsControl : UserControl
//    {
//        private Label lblOrderId;
//        private Label lblCustomer;
//        private Label lblStatus;
//        private ListBox lstOrderItems;

//        public OrderDetailsControl(string orderId, string customer, string status)
//        {
//            this.Dock = DockStyle.Fill;

//            // Labels for Order Details
//            lblOrderId = new Label
//            {
//                Text = $"Order ID: {orderId}",
//                Location = new Point(20, 20),
//                AutoSize = true,
//                Font = new Font("Segoe UI", 10, FontStyle.Bold)
//            };

//            lblCustomer = new Label
//            {
//                Text = $"Customer: {customer}",
//                Location = new Point(20, 50),
//                AutoSize = true
//            };

//            lblStatus = new Label
//            {
//                Text = $"Status: {status}",
//                Location = new Point(20, 80),
//                AutoSize = true
//            };

//            // ListBox for Order Items
//            lstOrderItems = new ListBox
//            {
//                Location = new Point(20, 120),
//                Size = new Size(340, 150),
//                Font = new Font("Segoe UI", 9)
//            };

//            // Dummy data for order items (In a real scenario, fetch this from the database)
//            lstOrderItems.Items.Add("Item 1: SaftMedKraft - Quantity: 2");
//            lstOrderItems.Items.Add("Item 2: KraftigSaft - Quantity: 1");
//            lstOrderItems.Items.Add("Item 3: LarsVingborgJuice - Quantity: 3");

//            // Back button to go back to the Orders list
//            Button btnBack = new Button
//            {
//                Text = "Back",
//                Location = new Point(280, 280),
//                Size = new Size(80, 30)
//            };
//            btnBack.Click += BtnBack_Click;

//            // Add controls to the UserControl
//            this.Controls.Add(lblOrderId);
//            this.Controls.Add(lblCustomer);
//            this.Controls.Add(lblStatus);
//            this.Controls.Add(lstOrderItems);
//            this.Controls.Add(btnBack);
//        }

//        // Event handler for the Back button
//        private void BtnBack_Click(object sender, EventArgs e)
//        {
//            // Assuming this control is hosted in a panel in the main form
//            var mainForm = this.FindForm() as Form1; // Adjust the cast if your form has a different name
//            mainForm?.ShowOrdersControl();
//        }
//    }
//}
