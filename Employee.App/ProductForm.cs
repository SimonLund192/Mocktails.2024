using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.App
{
    public class ProductForm : Form
    {
        public TextBox txtProductName;
        public TextBox txtPrice;
        public TextBox txtQuantity;
        public Button btnSave;
        public Button btnCancel;

        public ProductForm(string title = "Edit Product")
        {
            // Form settings
            this.Text = title;
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Padding = new Padding(20);

            // Create and position labels
            Label lblName = new Label
            {
                Text = "Name:",
                Location = new Point(30, 30),
                AutoSize = true
            };

            Label lblPrice = new Label
            {
                Text = "Price:",
                Location = new Point(30, 70),
                AutoSize = true
            };

            Label lblQuantity = new Label
            {
                Text = "Quantity:",
                Location = new Point(30, 110),
                AutoSize = true
            };

            // Initialize textboxes with proper positioning
            txtProductName = new TextBox
            {
                Location = new Point(100, 27),
                Size = new Size(250, 23)
            };

            txtPrice = new TextBox
            {
                Location = new Point(100, 67),
                Size = new Size(250, 23)
            };

            txtQuantity = new TextBox
            {
                Location = new Point(100, 107),
                Size = new Size(250, 23)
            };

            // Initialize buttons with proper positioning
            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(180, 160),
                Size = new Size(80, 30),
                DialogResult = DialogResult.OK
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(270, 160),
                Size = new Size(80, 30),
                DialogResult = DialogResult.Cancel
            };

            // Add all controls to form
            this.Controls.AddRange(new Control[]
            {
                lblName, lblPrice, lblQuantity,
                txtProductName, txtPrice, txtQuantity,
                btnSave, btnCancel
            });

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}
