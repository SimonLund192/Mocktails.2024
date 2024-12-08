using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Employee.App
{
    public class ProductForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBox txtProductName { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBox txtPrice { get; private set; }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBox txtQuantity { get; private set; }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBox txtDescription { get; private set; }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBox txtImageUrl { get; private set; }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Button btnSave { get; private set; }
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Button btnCancel { get; private set; }

        public ProductForm(string title = "Edit Product")
        {
            // Form settings
            Text = title;
            Size = new Size(400, 400);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Padding = new Padding(20);

            Controls.AddRange(new Control[]
            {
                CreateLabel("Name:", 30, 30),
                txtProductName = CreateTextBox(150, 30),

                CreateLabel("Description:", 30, 70),
                txtDescription = CreateTextBox(150, 70),

                CreateLabel("Price:", 30, 110),
                txtPrice = CreateTextBox(150, 110),

                CreateLabel("Quantity:", 30, 150),
                txtQuantity = CreateTextBox(150, 150),

                CreateLabel("Image URL:", 30, 190),
                txtImageUrl = CreateTextBox(150, 190),

                btnSave = CreateButton("Save", 180, 250, DialogResult.OK),
                btnCancel = CreateButton("Cancel", 270, 250, DialogResult.Cancel)
            });

            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true
            };
        }

        private TextBox CreateTextBox(int x, int y)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(200, 23)
            };
        }

        private Button CreateButton(string text, int x, int y, DialogResult dialogResult)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(80, 30),
                DialogResult = dialogResult
            };
        }
    }
}
