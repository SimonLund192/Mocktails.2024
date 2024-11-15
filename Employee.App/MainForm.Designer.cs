namespace Employee.App
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            btnProducts = new Button();
            btnOrders = new Button();
            btnDashboard = new Button();
            mainPanel = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnProducts);
            panel1.Controls.Add(btnOrders);
            panel1.Controls.Add(btnDashboard);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.MaximumSize = new Size(200, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(156, 450);
            panel1.TabIndex = 0;
            // 
            // btnProducts
            // 
            btnProducts.Location = new Point(35, 61);
            btnProducts.Name = "btnProducts";
            btnProducts.Size = new Size(75, 23);
            btnProducts.TabIndex = 3;
            btnProducts.Text = "Products";
            btnProducts.UseVisualStyleBackColor = true;
            btnProducts.Click += btnProducts_Click;
            // 
            // btnOrders
            // 
            btnOrders.Location = new Point(35, 100);
            btnOrders.Name = "btnOrders";
            btnOrders.Size = new Size(75, 23);
            btnOrders.TabIndex = 2;
            btnOrders.Text = "Orders";
            btnOrders.UseVisualStyleBackColor = true;
            btnOrders.Click += btnOrders_Click;
            // 
            // btnDashboard
            // 
            btnDashboard.Location = new Point(35, 22);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(75, 23);
            btnDashboard.TabIndex = 0;
            btnDashboard.Text = "Dashboard";
            btnDashboard.UseVisualStyleBackColor = true;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // mainPanel
            // 
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(156, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(644, 450);
            mainPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(mainPanel);
            Controls.Add(panel1);
            Name = "MainForm";
            Text = "Form1";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnOrders;
        private Button btnDashboard;
        private Panel mainPanel;
        private Button btnProducts;
    }
}
