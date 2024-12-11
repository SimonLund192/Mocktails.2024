namespace Employee.App
{
    public partial class DashboardControl : UserControl
    {

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            flowStats = new FlowLayoutPanel();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            dgvRecentOrders = new DataGridView();
            btnInspectOrder = new Button();
            flowStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecentOrders).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(119, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Dashboard";
            // 
            // flowStats
            // 
            flowStats.AutoScroll = true;
            flowStats.Controls.Add(panel1);
            flowStats.Controls.Add(panel2);
            flowStats.Controls.Add(panel3);
            flowStats.Dock = DockStyle.Top;
            flowStats.Location = new Point(0, 30);
            flowStats.MaximumSize = new Size(0, 100);
            flowStats.Name = "flowStats";
            flowStats.Size = new Size(702, 100);
            flowStats.TabIndex = 1;
            flowStats.Paint += flowStats_Paint;
            // 
            // panel1
            // 
            panel1.Location = new Point(10, 10);
            panel1.Margin = new Padding(10);
            panel1.MaximumSize = new Size(200, 80);
            panel1.Name = "panel1";
            panel1.Size = new Size(180, 80);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.Location = new Point(210, 10);
            panel2.Margin = new Padding(10);
            panel2.MaximumSize = new Size(200, 80);
            panel2.Name = "panel2";
            panel2.Size = new Size(180, 80);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Location = new Point(410, 10);
            panel3.Margin = new Padding(10);
            panel3.Name = "panel3";
            panel3.Size = new Size(180, 80);
            panel3.TabIndex = 2;
            // 
            // dgvRecentOrders
            // 
            dgvRecentOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecentOrders.Dock = DockStyle.Top;
            dgvRecentOrders.Location = new Point(0, 130);
            dgvRecentOrders.Name = "dgvRecentOrders";
            dgvRecentOrders.Size = new Size(702, 105);
            dgvRecentOrders.TabIndex = 2;
            dgvRecentOrders.CellContentClick += dgvRecentOrders_CellContentClick;
            // 
            // btnInspectOrder
            // 
            btnInspectOrder.Location = new Point(10, 241);
            btnInspectOrder.Name = "btnInspectOrder";
            btnInspectOrder.Size = new Size(75, 23);
            btnInspectOrder.TabIndex = 3;
            btnInspectOrder.Text = "Inspect";
            btnInspectOrder.UseVisualStyleBackColor = true;
            // 
            // DashboardControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnInspectOrder);
            Controls.Add(dgvRecentOrders);
            Controls.Add(flowStats);
            Controls.Add(lblTitle);
            Name = "DashboardControl";
            Size = new Size(702, 392);
            flowStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRecentOrders).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion



        private Label lblTitle;
        private FlowLayoutPanel flowStats;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private DataGridView dgvRecentOrders;
        private Button btnInspectOrder;
    }
}
